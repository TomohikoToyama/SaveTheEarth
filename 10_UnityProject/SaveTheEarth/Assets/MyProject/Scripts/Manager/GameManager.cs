using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ゲームの状態
    enum STATE
    {
        MENU   = 0,
        STAGE  = 1,
        RESULT = 2
    }

    //キャンバス一覧
    enum CANVAS
    {
        MENU   = 0,
        STAGE  = 1,
        RESULT = 2
    }

    int currentState;
    [SerializeField] private EnemyManager enmM;  //敵の管理
    [SerializeField] private StageManager stgM;  //ステージの管理
    [SerializeField] private MenuManager  menuM; //メニューの管理
    [SerializeField] private ManagerMain  rankingM; //ランキングの管理

    // Start is called before the first frame update
    void Start()
    {
      //  rankingM.OnClickGetRankingApi();
        currentState = (int)STATE.MENU;
        menuM.Init();   
        stgM.Init();
    }

    //更新処理
    void Update()
    {
        StateUpdate();

    }

    /*
     * 各種状態にあった管理クラスの処理を走らせる
     * 
    */
    private void StateUpdate()
    {
        if(currentState == (int)STATE.MENU)         //メニューでの処理
        {
            menuM.StateUpdate();

        }
        else if(currentState == (int)STATE.STAGE)   //ステージでの処理
        {
            stgM.StateUpdate();
            enmM.StateUpdate();

        }
        else if(currentState == (int)STATE.RESULT)  //リザルトでの処理
        {

        }
    }

    //状態が変わった時の処理
    public void StateChange()
    {
        if (currentState == (int)STATE.MENU)
        {
            currentState = (int)STATE.STAGE;
        }
        else if (currentState == (int)STATE.STAGE)
        {
            currentState = (int)STATE.RESULT;
        }
        else if (currentState == (int)STATE.RESULT)
        {
            currentState = (int)STATE.MENU;
        }
    }

    public int GetGameCurrent()
    {
        return currentState;
    }

    public void SetMenu()
    {
        currentState = (int)STATE.MENU;
    }
    public void SetStage()
    {
        currentState = (int)STATE.STAGE;
    }
    public void SetResult()
    {
        currentState = (int)STATE.RESULT;
    }
    public void GetStage()
    {

    }

    //シングルトン化
    protected static GameManager instance;
    public static GameManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));

                if (instance == null)
                {
                    Debug.LogError("GameManager Instance Error");

                }
            }

            return instance;
        }

    }
}
