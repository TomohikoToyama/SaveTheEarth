using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    bool dialogFlg; //ダイアログが開閉状態か
    enum DIATYPE
    {
       STAGE   = 0,
       RANKING = 1
    }

    [SerializeField]Canvas menuCanvas;          
    [SerializeField]GameObject StageDialog;
    [SerializeField]GameObject RankingDialog;

    StageData selectStg;    //選択したステージ情報
    //初期化処理
    public void Init()
    {
        
    }

    //更新処理
    public void StateUpdate()
    {
        if (dialogFlg)
            return;
    }

    //ステージダイアログを開く
    public void OpenStageDialog(StageData data)
    {
        if (dialogFlg)
            return;

        selectStg = data;
        dialogFlg = true;
        StageDialog.SetActive(true);
    }

    //ステージダイアログを閉じる
    public void CloseStageDialog()
    {
        dialogFlg = false;
        StageDialog.SetActive(false);
        StageManager.Instance.ReleaseStage();
    }


    //シングルトン化
    protected static MenuManager instance;
    public static MenuManager Instance
    {

        get
        {
            if (instance == null)
            {
                instance = (MenuManager)FindObjectOfType(typeof(MenuManager));

                if (instance == null)
                {
                    Debug.LogError("MenuManager Instance Error");

                }
            }

            return instance;
        }

    }
}
