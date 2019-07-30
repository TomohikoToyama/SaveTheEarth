using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralButton : MonoBehaviour
{
    //シーンのenum
    enum STATE
    {
        MENU  = 0,
        STAGE = 1
    }

    //ボタン名のenum
    enum BUTTON{
        Positive,
        Negative,
        Resume,
        Retry,
        Menu

    }
    public string buttonName;
    private int currentState;
    private StageManager stgM;
    private MenuManager menuM;

    // Start is called before the first frame update
    void Start()
    {
        stgM = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        menuM = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        buttonName = this.gameObject.name;
    }

    //ボタンに触れた時の処理
    public void OnClick()
    {
        //はいボタンを押した時の処理
        if (buttonName == BUTTON.Positive.ToString())
        {

            if ((int)STATE.MENU == GameManager.Instance.GetGameCurrent())
            {
                stgM.PrepareStage();
                menuM.CloseStageDialog();
            }
            else if ((int)STATE.STAGE == GameManager.Instance.GetGameCurrent())
            {
                stgM.RetryStage();
            }
        }
        //いいえボタンを押した時の処理
        else if (buttonName == BUTTON.Negative.ToString())
        {
            if ((int)STATE.MENU == GameManager.Instance.GetGameCurrent())
            {
                menuM.CloseStageDialog();
            }
            else if ((int)STATE.STAGE == GameManager.Instance.GetGameCurrent())
            {
                stgM.GoMenu();
            }
        }
        else if (buttonName == BUTTON.Retry.ToString())
        {
            //menuM.CloseStageDialog();
            stgM.RetryStage();
        }
        else if (buttonName == BUTTON.Menu.ToString())
        {
            //menuM.CloseStageDialog();
            stgM.GoMenu();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        OnClick();
    }

}
