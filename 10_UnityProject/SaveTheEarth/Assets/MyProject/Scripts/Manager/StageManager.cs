using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StageManager : MonoBehaviour
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
    int currentState;       //現在の状態
    [SerializeField] Transform canvasPos;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject stgSpawner;
    [SerializeField] GameObject menuObj;
    [SerializeField] GameObject effectObj;
    public float battleTime; //戦闘時間
    GameObject stgObj;      //
    [SerializeField] GameObject RetryBoard;
    [SerializeField] Text scoreText;
    [SerializeField] Text TimeText;
    StageData selectStg;    //選択したステージ情報
    [SerializeField] GameObject stgCanvas;
    [SerializeField] GameObject resultCanvas;
    bool stgSelectFlg;    //メニュー画面で
    //private List<GameObject> stgObj = new List<GameObject>();     //ステージ遷移用オブジェクト
    public List<StageData> stgDataList = new List<StageData>(); //ステージデータ一覧
    public List<GameObject> prefObjList = new List<GameObject>();
    private int clearFlg;   //ステージのクリア状態
    private string stgPath = "StageData/Stage_";
    private string stgPref = "Prefabs/Stage_";
    [SerializeField] EnemyManager enmM;
    [SerializeField] ManagerMain rankingM;
    // Start is called before the first frame update
    public void Init()
    {
        clearFlg = 2;
        SetStageInfo();
        battleTime = 60f;       //制限時間

    }

    //更新処理
    public void StateUpdate()
    {
        if (currentState == (int)STATE.PLAY)
        {
            if (selectStg.Health <= 0)
            {
                ChangeState((int)STATE.GAMEOVER);
            }
            ReduceTime();
            if (battleTime <= 0f)
            {
               // TimeUp();
            }

        }
        else if (currentState == (int)STATE.PAUSE)       //ポーズをかけてる時の処理
        {   

        }
        else if (currentState == (int)STATE.BOSS)        //ボス戦での処理
        {

        }else if (currentState == (int)STATE.RESULT)    
        {

        }
        else if (currentState == (int)STATE.GAMEOVER)   //
        {

        }
    }

    public int GetStageState()
    {
        return currentState;
    }

    //メニュー画面での処理
    #region
    //選択したステージ情報を設定
    public void SelectStage(StageData data)
    {
        //重複して開かないように
        if (stgSelectFlg)
            return;

        stgSelectFlg = true;
        selectStg = data;
        MenuManager.Instance.OpenStageDialog(data);
    }

    //ステージの選択を解除
    public void ReleaseStage()
    {
        selectStg.SetDefault();
        stgSelectFlg = false;

    }
    //ステージ情報を表示する
    private void SetStageInfo()
    {
        for (int i = 1; i <= clearFlg; i++)
        {
            var patName = stgPref + i;
            GameObject obj = (GameObject)Resources.Load(patName);
            var stgObj = Instantiate(obj);
            prefObjList.Add(stgObj);
            stgObj.transform.parent = menuObj.transform;
            var stgData = stgObj.GetComponent<StageData>();
            stgData.Init();
            stgDataList.Add(stgData);
        }
    }

    //選択中かのフラグ
    public bool GetSelectFlg()
    {
        return stgSelectFlg;
    }

    //ステージを展開する
    public void PrepareStage()
    {
        //ステージ選択用のオブジェクトを非アクティブ化
        for (int i = 0; i < prefObjList.Count; i++)
        {
            prefObjList[i].SetActive(false);
        }
        SoundManager.Instance.PlaySE(4);
        selectStg.StageInit();
        currentState = (int)STATE.PREPARE;  //ステージ準備
        StartCoroutine(SetPlayer());        //プレイヤーの位置の設定
        SceneManager.LoadSceneAsync("Stage_1", LoadSceneMode.Additive); //ステージ情報を非同期で読み込み
        ApperStgCanvas();                   //ステージでの表示用のUI表示
    }

    //プレイヤーの位置を設定する(仮)
    private IEnumerator SetPlayer()
    {
        effectObj.SetActive(true);                                          //VR酔い対策用のUIを表示する
        float pos = 0;
        while (pos < 12.5f)
        {
            pos += 0.125f;
            menuObj.transform.position = new Vector3(0, -pos * 0.1f, 0);    //ポッドを下降させる
            Player.transform.position = new Vector3(0, pos, 0);             //プレイヤーを上昇させる
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.SetStage();    //ゲーム管理クラスをステージ状態にする                              
        ChangeState((int)STATE.READY);      //ステージ管理クラスを準備状態にする
        StartCoroutine(PlayReady());        //戦闘開始準備用のコルーチンを動作させる
        menuObj.SetActive(false);           //格納用ポッドを非表示にする
        effectObj.SetActive(false);         //VR酔い対策用のUIを非表示にする
    }
    //プレイヤーの位置を設定する(仮)
    private IEnumerator SetMenu()
    {
        effectObj.SetActive(true);      //VR酔い対策用のUIを表示する
        menuObj.SetActive(true);        //格納用ポッドを表示する
        float pos = 12.5f;
        while (pos > 1)
        {
            pos -= 0.125f;
            menuObj.transform.position = new Vector3(0, -pos * 0.1f, 0);    //ポッドを上昇させる
            Player.transform.position = new Vector3(0, pos, 0);             //プレイヤーを下降させる
            yield return new WaitForSeconds(0.01f);
        }
        Player.transform.position = new Vector3(0, 1, 0); //プレイヤーを初期位置に設定する
        effectObj.SetActive(false);                       //VR酔い対策用のUIを非表示にする
        SceneManager.UnloadSceneAsync(("Stage_1"));       //ステージ画面を非表示にする
    }

    //ランキング取得処理
    public void GetRanking()
    {

    }

    //ランキング設定処理
    public void SetRanking()
    {

    }
    //
    public void SetScore(int point)
    {
        selectStg.Score += point;
        scoreText.text = "SCORE：" + selectStg.Score.ToString();

    }

    private IEnumerator PlayReady()
    {
        SoundManager.Instance.PlayBGM(0);
        yield return new WaitForSeconds(3f);
        enmM.Init();
        ChangeState((int)STATE.PLAY);
    }

    #endregion

    //プレイ中の処理群
    #region
    public void DamageStage()
    {
        selectStg.GetDamage();
    }
        
    //ステージプレイ中用のキャンバス表示
    private void ApperStgCanvas()
    {
        stgCanvas.SetActive(true);
    }


    //ステージプレイ中用のキャンバス非表示
    private void DisapperStgCanvas()
    {
        stgCanvas.SetActive(false);
    }

    //リザルト用のキャンバス表示
    private void ApperResultCanvas()
    {
        Vector3 pos = canvasPos.position;
        resultCanvas.transform.position = new Vector3(pos.x, pos.y,pos.z);
        resultCanvas.SetActive(true);
    }

    //リザルト用のキャンバス非表示
    private void DisapperResultCanvas()
    {
        resultCanvas.SetActive(false);
    }

    public void PlayGameOver()
    {
        RetryBoard.SetActive(true);
    }

    //時間の
    private void ReduceTime()
    {
        battleTime -= Time.deltaTime;
        TimeText.text = "残り" + battleTime.ToString("f0") + "秒";
    }

    //時間切れになった時の処理
    private void TimeUp()
    {
        EnemyManager.Instance.DeleteEnemy();
      //  rankingM.OnClickSetRankingApi(selectStg.Score);
       // rankingM.OnClickGetRankingApi();
        currentState = (int)STATE.RESULT;
        GameManager.Instance.SetResult();
        ApperResultCanvas();
        DisapperStgCanvas();
    }

    //ランキング名前入力用の処理
    private void InputName()
    {

    }

    private void ChangeState(int state)
    {
            currentState = state;
     }


    //リトライ用の処理
    public void RetryStage()
    {
        Init();
        StartCoroutine(PlayReady());
        ApperStgCanvas();
        DisapperResultCanvas();
        RetryBoard.SetActive(false);
        selectStg.StageInit();
        ChangeState((int)STATE.READY);
        GameManager.Instance.SetStage();
        
    }


    //メニュー画面の遷移処理
    public void GoMenu()
    {
        SoundManager.Instance.StopBGM();    //バトルBGMを停止
        SetScore(0);                        //スコアを初期化
        ChangeState((int)STATE.NONE);       //ステージの状態を初期化
        battleTime = 60f;                   //戦闘時間の初期化
        DisapperResultCanvas();             //リザルドのUIを非表示にする
        RetryBoard.SetActive(false);        //リトライ用のUIを非表示にする
        StartCoroutine(SetMenu());          //メニューの場所に移動するコルーチンを動かす
        GameManager.Instance.SetMenu();     //ゲーム管理クラスの状態をメニューにする
        for (int i = 0; i < prefObjList.Count; i++)
        {
            prefObjList[i].SetActive(true);
        }

    }


    public void Ranking()
    {

    }
    #endregion

    //ボス戦での処理群


    //シングルトン化
    protected static StageManager instance;
    public static StageManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = (StageManager)FindObjectOfType(typeof(StageManager));

                if (instance == null)
                {
                    Debug.LogError("StageManager Instance Error");

                }
            }

            return instance;
        }

    }

}
