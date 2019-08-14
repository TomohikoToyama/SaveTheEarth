using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    bool setFlg;
    float spawnTime = 2f;
    float currenTime;
    [SerializeField] EnemySpawner enmSpawner;
    [SerializeField] StageManager stgM;

    //初期化処理
    public void Init()
    {
        currenTime = 0;
        enmSpawner.Init();
        stgM = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
    }

    //更新処理
    public void StateUpdate()
    {
        if (!setFlg)
            return;


        currenTime += Time.deltaTime;
        if(currenTime >= spawnTime)
        {
            spawnTime = Random.Range(0.3f,1.2f);
            enmSpawner.Spawn();
            currenTime = 0;
        }
        
    }


    //プレイ開始で敵を生成する準備が出来た際にTrueにする
    public void ChangeFlg()
    {
        setFlg = true;

    }
    public void DeleteEnemy()
    {
        enmSpawner.DeleteAll();
    }

    public void SendScore(int point)
    {
        stgM.SetScore(point);

    }
    //シングルトン化
    protected static EnemyManager instance;
    public static EnemyManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = (EnemyManager)FindObjectOfType(typeof(EnemyManager));

                if (instance == null)
                {
                    Debug.LogError("StageManager Instance Error");

                }
            }

            return instance;
        }

    }
}
