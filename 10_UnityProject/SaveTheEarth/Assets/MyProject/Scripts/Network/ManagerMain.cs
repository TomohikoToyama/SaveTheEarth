using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text
using System;
using UnityEngine.Networking;

/// <summary>
/// Manager main.
/// </summary>
public class ManagerMain : MonoBehaviour
{
    [SerializeField] private Text displayField;
    [SerializeField] private Text inputName;
    [SerializeField] private int  inputScore;
    [SerializeField] private List<string> nameList = new List<string>();
    [SerializeField] private List<int> getScoreList = new List<int>();
    [SerializeField] private List<Text> setScoreList = new List<Text>();
    [SerializeField] private List<Text> setNameList = new List<Text>();
    const string IPAddres     = "http://localhost";
    const string getAPIpath   = "/SteApi/Steranking/getranking";
    const string setAPIpath   = "/SteApi/Steranking/setranking";

    int setScore;
    /// <summary>
    /// Raises the click clear display event.
    /// </summary>
    public void OnClickClearDisplay()
    {
        displayField.text = " ";
    }

    /// <summary>
    /// Raises the click get json from www event.
    /// </summary>
    public void OnClickGetRankingApi()
    {
        displayField.text = "wait...";
        GetJsonFromWww();
    }

    /// <summary>
    /// Raises the click get json from www event.
    /// </summary>
    public void OnClickSetRankingApi(int finalScore)
    {
        setScore = finalScore;
        displayField.text = "wait...";
        SetJsonFromWww();
    }

    /// <summary>
    /// Gets the json from www.
    /// </summary>
    private void GetJsonFromWww()
    {
        // APIが設置してあるURLパス
        const string url = IPAddres + getAPIpath;

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(GetRanking(url, CallbackWwwSuccess, CallbackWwwFailed));
    }

    /// <summary>
    /// Callbacks the www success.
    /// </summary>
    /// <param name="response">Response.</param>
    private void CallbackWwwSuccess(string response)
    {
        int num = 0;
        // json データ取得が成功したのでデシリアライズして整形し画面に表示する
        List<MessageData> messageList = MessageDataModel.DeserializeFromJson(response);

        string sStrOutput = "";
        foreach (MessageData message in messageList)
        {
            
            nameList.Add(message.Name);
            getScoreList.Add(message.Score);
            setScoreList[num].text = message.Score.ToString();
            setNameList[num].text = message.Name;
            num++;
        }

        displayField.text = sStrOutput;
    }

    /// <summary>
    /// Callbacks the www failed.
    /// </summary>
    private void CallbackWwwFailed()
    {
        // jsonデータ取得に失敗した
        displayField.text = "Www Failed";
    }

    /// <summary>
    /// Callbacks the API success.
    /// </summary>
    /// <param name="response">Response.</param>
    private void CallbackApiSuccess(string response)
    {
        // json データ取得が成功したのでデシリアライズして整形し画面に表示する
        displayField.text = response;
    }

    /// <summary>
    /// Downloads the json.
    /// </summary>
    /// <returns>The json.</returns>
    /// <param name="url">S tgt UR.</param>
    /// <param name="cbkSuccess">Cbk success.</param>
    /// <param name="cbkFailed">Cbk failed.</param>
    private IEnumerator GetRanking(string url, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        // WWWを利用してリクエストを送る
        var webRequest = UnityWebRequest.Get(url);

        //タイムアウトの指定
        webRequest.timeout = 5;

        // WWWレスポンス待ち
        yield return webRequest.SendWebRequest();

        if (webRequest.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(webRequest.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (webRequest.isDone)
        {
            // リクエスト成功の場合
            Debug.Log($"Success:{webRequest.downloadHandler.text}");
            if (null != cbkSuccess)
            {
                cbkSuccess(webRequest.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// Response the check for time out WWW.
    /// </summary>
    /// <returns>The check for time out WWW.</returns>
    /// <param name="webRequest">Www.</param>
    /// <param name="timeout">Timeout.</param>
    private IEnumerator ResponseCheckForTimeOutWWW(UnityWebRequest webRequest, float timeout)
    {
        float requestTime = Time.time;

        while (!webRequest.isDone)
        {
            if (Time.time - requestTime < timeout)
            {
                yield return null;
            }
            else
            {
                Debug.LogWarning("TimeOut"); //タイムアウト
                break;
            }
        }

        yield return null;
    }


    /// <summary>
    /// Sets the json from www.
    /// </summary>
    private void SetJsonFromWww()
    {
        // APIが設置してあるURLパス
        string sTgtURL = IPAddres + setAPIpath;

        string name = "NONAME";
        int score = setScore;

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(SetRanking(sTgtURL, name, score, CallbackApiSuccess, CallbackWwwFailed));
    }

    /// <summary>
    /// Sets the message.
    /// </summary>
    /// <returns>The message.</returns>
    /// <param name="url">URL target.</param>
    /// <param name="name">Name.</param>
    /// <param name="message">Message.</param>
    /// <param name="cbkSuccess">Cbk success.</param>
    /// <param name="cbkFailed">Cbk failed.</param>
    private IEnumerator SetRanking(string url, string name, int score, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Score", score);

        // WWWを利用してリクエストを送る
        var webRequest = UnityWebRequest.Post(url, form);

        //タイムアウトの指定
        webRequest.timeout = 5;

        // WWWレスポンス待ち
        yield return webRequest.SendWebRequest();

        if (webRequest.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(webRequest.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (webRequest.isDone)
        {
            
            
            
            // リクエスト成功の場合
            Debug.Log($"Success:{webRequest.downloadHandler.text}");
            if (null != cbkSuccess)
            {
                cbkSuccess(webRequest.downloadHandler.text);
            }
        }
    }
}