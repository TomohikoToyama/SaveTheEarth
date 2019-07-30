using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    private float targetX;
    private float targetY;
    private float targetZ;
    private Transform targetPos;
    private float speed;
    private float fallSpeed;
    Vector3 firstPos = new Vector3(5, 12.5f, 0);
    bool deadFlg;
    [SerializeField] GameObject explosion;
    bool moveFlg;

    private int _health;        //体力
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

    private int currentState;
    enum STATE
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
        
        if(moveFlg)
         EnemyMover();
    }


    //初期化処理
    public void Init()
    {
        currentState = (int)STATE.MOVE;
        Point = 100;
        targetPos = GameObject.FindGameObjectWithTag("Stage").transform;
        targetX = Random.Range(-2, 1);
        targetY = 0;
        targetZ = -10;
        speed = Random.Range(0.1f, 3);
        var firstPosX = Random.Range(-2.5f, 2.5f);
        var firstPosY = Random.Range(12, 15);
        var firstPosZ = Random.Range(-2, 1);

        firstPos = new Vector3(firstPosX, firstPosY, firstPosZ);
        fallSpeed = 0.2f;
        targetPos.position = new Vector3(targetX, targetY, targetZ);
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
        Health = 1;
        Point = point;
    }
    //ダメージを受けた時の処理
    private void Damage()
    {
        if(!moveFlg)
            return;

        StartCoroutine(InviMode());
        Health --;
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
        SoundManager.Instance.PlaySE(3);
        var obj = Instantiate(explosion, transform.position, transform.rotation);
        transform.position = new Vector3(1000,1000,1000);
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
        gameObject.SetActive(false);
    }
    //ステージに攻撃や本体がヒットした時の処理
    private void HitStage()
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
        yield return new WaitForSeconds(0.05f);
        InviFlg = false;
    }

}
