using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyManager enmM;
    [SerializeField] GameObject enemy;
    public List<GameObject> enmList = new List<GameObject>(); 
    Vector3 spwanPoint = new Vector3(0,12.5f,100);   //仮の発生地点
    int spawnNum;
    int enemyNum = 50;

    //初期化処理
    public void Init()
    {
        spawnNum = 0;
      //  for (int i = 0; i < enemyNum; i++)
    //    {
     //       var enmObj = Instantiate(enemy, spwanPoint, Quaternion.identity);
      //      enmList.Add(enmObj);
      //  }
        enmM.ChangeFlg();
    }

    //敵生成処理
    public void Spawn()
    {
        var enmObj = ObjectPool.instance.GetGameObject(enemy, new Vector3(0,12.5f,100), transform.rotation);
        enmObj.GetComponent<EnemyData>().Init();
        /*
        if (enmList.Count > spawnNum)
        {

            enmList[spawnNum].GetComponent<EnemyData>().Init();
            spawnNum ++;
        }
        */
    }

    //敵を全て破棄する(メニュー画面に遷移した際や、リトライ用)
    public void DeleteAll()
    {
        for(int i = 0; i < enmList.Count; i++)
        {
            Destroy(enmList[i]);
        }
        enmList.Clear();
    }
    
}
