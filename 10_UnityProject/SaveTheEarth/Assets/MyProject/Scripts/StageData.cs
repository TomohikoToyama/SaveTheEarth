using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class StageData : MonoBehaviour
{
    //ステージ管理の状態
    enum STATE
    {
        NONE = 0,
        PREPARE = 1,
        READY = 2,
        PLAY = 3,
        PAUSE = 4,
        RETRY = 5,
        BOSS = 6,
        RESULT = 7,
        GAMEOVER = 8
    }

    //接触オブジェクト
    enum Hit
    {
        Hand,
        Enemy
    }


    StageManager stgM;
    Material mat;        //マテリアル
    Color    defColor;   //非接触時に元の色に戻す
    string stgName;      //ステージの名称
    int    HighScore; //ステージのハイスコア
    private int _score;     //ステージの現在のスコア
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    private int _health;
    public  int    Health;    //ステージの体力
    bool   stgFlg;       //ステージクリア済みかのフラグ

    //スクリプト自体の初期化
    public void Init()
    {
        stgM = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        //初期の色に戻す為の設定をする
        mat = GetComponent<Renderer>().material;
        defColor = mat.color;

    }

    //ステージの初期化
    public void StageInit()
    {
        Health = 1;
    }
    

    //敵から攻撃を受けた時の処理
    public void GetDamage()
    {
        Score -= 50;

        //ステージ体力が0になったらゲームオーバー処理
        if(Health <= 0)
        {
            stgM.PlayGameOver();
        }
    }

    private void Retry()
    {

    }


    //元の色に戻す
    public void SetDefault()
    {
        mat.color = defColor;
    }

    //接触判定
    public void OnTriggerEnter(Collider other)
    {

        //メニューシーンで、ステージ生成用オブジェクトと接触したのを分かり易くする為の色変更
        if ((int)STATE.NONE == stgM.GetStageState())
        {
            if (stgM.GetSelectFlg())
                return;

            if (other.tag == Hit.Hand.ToString())
            {
                mat.color = new Color(255, 255, 255);
                stgM.SelectStage(this);
            }
        }
        else if ((int)STATE.PLAY == stgM.GetStageState())
        {
            if (other.tag == Hit.Enemy.ToString())
            {
                GetDamage();
            }
        }
    }

}
