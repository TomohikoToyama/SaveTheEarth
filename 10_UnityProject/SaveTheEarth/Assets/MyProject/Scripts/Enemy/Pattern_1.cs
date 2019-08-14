using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;
public class Pattern_1 : EnemyData,IEnemyPattern
{

    MeshRenderer mesh;
    float startTime;
    float limitTime;
    public  void Init()
    {
        Debug.Log("test");
        currentState = (int)STATE.MOVE;
        Point = 100;
        targetPos = GameObject.FindGameObjectWithTag("Stage").transform;
        targetX = Random.Range(-2, 1);
        targetY = 0;
        targetZ = -10;
        speed = Random.Range(0.1f, 3);
        firstPosX = Random.Range(-2, 2);
        firstPosZ = Random.Range(-2, 1);
        firstPosY = Random.Range(12, 15);

        firstPos = new Vector3(firstPosX, firstPosY, firstPosZ);
        SetData(3, 100);
        fallSpeed = 0.2f;
        targetPos.position = new Vector3(targetX, targetY, targetZ);
        currentState = (int)STATE.MOVE;
        startTime = Random.Range(0,3f);
        
    }

    void Update()
    {

        limitTime += Time.deltaTime;
        if (startTime <= limitTime)
            moveFlg = true;

        if (!moveFlg)
            return;
        Move();
    }

    public void SetPos()
    {
        targetX = Random.Range(-2, 1);
        targetY = 0;
        targetZ = -10;
        speed = Random.Range(0.1f, 3);
        firstPosX = Random.Range(-2, 2);
        firstPosZ = Random.Range(-2, 1);
        firstPosY = Random.Range(12, 15);
        targetPos.position = new Vector3(targetX, targetY, targetZ);
        firstPos = new Vector3(firstPosX, firstPosY, firstPosZ);

    }
    //移動パターン
    public void Move()
    {
        if (currentState == (int)STATE.MOVE)
        {
            transform.position = Vector3.Lerp(transform.position, firstPos, speed * Time.deltaTime);
            if (transform.position.z <= 1f)
            {

                currentState = (int)STATE.ATTACK;
            }
        }
        else if (currentState == (int)STATE.ATTACK)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos.position, fallSpeed * Time.deltaTime);

            if (transform.position.y <= 5f)
            {
                HitStage();
            }
        }
    }

    //攻撃パターン
    public void Atack()
    {

    }
}
