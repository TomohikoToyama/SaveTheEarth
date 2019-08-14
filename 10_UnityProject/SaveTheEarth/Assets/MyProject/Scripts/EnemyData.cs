using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField]MeshRenderer mesh;
    public float targetX;
    public float targetY;
    public float targetZ;
    public float firstPosX;
    public float firstPosY; 
    public float firstPosZ;
    public Vector3 DefaultPos;
    public Transform targetPos;
    public float speed;
    public float fallSpeed;
    public Vector3 firstPos = new Vector3(5, 12.5f, 0);
    bool deadFlg;
    [SerializeField] GameObject explosion;
    public bool moveFlg;

    private int _health = 3;        //体力
    public  int Health {
        get { return _health; }
        set { _health = value; }
    }

    private int _point;         //スコアポイント
    public int Point
    {
        get { return _point; }
        set { _point = value; }
    }

    private float _inviTime;  //無敵時間
    public float InviTime
    {
        get { return _inviTime; }
        set { _inviTime = value; }
    }

    private bool _inviFlg;   //無敵判定
    public bool InviFlg
    {
        get { return _inviFlg; }
        set { _inviFlg = value; }
    }

    public int currentState;
    public enum STATE
    {
        MOVE   = 0,
        ATTACK = 1
    }

    //衝突判定用の
    private enum HitObj{
        Hand,
        Stage
    }


    // Update is called once per frame
    void Update()
    {

        if (!moveFlg)
            return;
       // EnemyMover();
    }


    //初期化処理
    public  void Init()
    {
        GetComponent<IEnemyPattern>().Init();
        DefaultPos = new Vector3(0,12.5f,25);
        targetPos = GameObject.FindGameObjectWithTag("Stage").transform;
        SetData(3, 100);
        //SetPos();
        // var pattern = gameObject.GetComponent<>();
       currentState = (int)STATE.MOVE;

       fallSpeed = 0.2f;
       
       moveFlg = true;
    
    }

    
    //敵の移動処理
    public void EnemyMover()
    {
        if (currentState == (int)STATE.MOVE) {
            transform.position = Vector3.Lerp(transform.position, firstPos, speed * Time.deltaTime);
            if (transform.position.z <= 1f)
            {
                currentState = (int)STATE.ATTACK;
            }
        } else if (currentState == (int)STATE.ATTACK) { 
            transform.position = Vector3.Lerp(transform.position, targetPos.position, fallSpeed * Time.deltaTime);

            if (transform.position.y <= 5f)
            {
                HitStage();
            }
        }



    }
    public void SetData(int hp, int point)
    {
        Health = 3;
        Point = point;
    }
    //ダメージを受けた時の処理
    private void Damage()
    {
        if(!moveFlg || InviFlg)
            return;

        StartCoroutine(InviMode());
        //体力が0になったら死亡

        if (Health <= 0)
        {
            EnemyManager.Instance.SendScore(Point);
            StartCoroutine(Explosion());
            
        }
        
    }

    private IEnumerator Explosion()
    {
        moveFlg = false;
        currentState = (int)STATE.MOVE;
        SoundManager.Instance.PlaySE(3);
        var obj = Instantiate(explosion, transform.position, transform.rotation);
        transform.position = new Vector3(1000,1000,1000);
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
        ObjectPool.instance.ReleaseGameObject(gameObject);
        //gameObject.SetActive(false);
    }
    //ステージに攻撃や本体がヒットした時の処理
    public void HitStage()
    {
        StageManager.Instance.DamageStage();
        StartCoroutine(Explosion());
    }

    private void OnTriggerStay(Collider other)
    {
        

        if(other.tag == HitObj.Hand.ToString())
        {
            if (InviFlg)
                return;

            Damage();

        }else if (other.tag == HitObj.Stage.ToString())
        {
            HitStage();
        }
    }

    //エフェクト
    private IEnumerator DamageEffect()
    {

        return null;
    }

    private IEnumerator InviMode()
    {

        InviFlg = true;
        Health--;
        mesh.material.color = new Color(1,0,0,1);
        yield return new WaitForSeconds(0.1f);
        mesh.material.color = new Color(1, 1, 1, 1);
        InviFlg = false;
    }

}
