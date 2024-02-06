using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class C_Game : MonoBehaviour
{
    // これが表示されている間動かない
    [SerializeField] GameObject panel;

    [SerializeField] RectTransform[] List;
    [SerializeField] RectTransform[] textspd;

    RectTransform obj;

    // SE関連
    [SerializeField] Mng_Game Sound;

    [SerializeField] GameObject ConfigP;
    float x, y;
    // textspd用
    int num = 1;
    int Copynum=1;
    // List用
    int Listnum = 0;
    // switch
    int method = 0;

    // Slow:0.08 Normal:0.04 Fast:0.02
    float Set_textSpd =0.04f;
    float Send_textSpd =0.04f;

    // TalkUIが完成したらプレファブを差し込む
    [SerializeField]TextScript textScr;

    //サンプルテキスト関連
    private string[] sampleMessage= { "テキスト速度を変更できます。\nテキスト制作担当より" };
    private int sampleNum;
    private int nowSampleNum = 0;
    private float textSpeed = 0f;
    private float elapsedTime = 0f;
    private bool isOneMessage = false;
    private bool isEndMessage = false;
    [SerializeField] private Text sampleMessageText;
    private bool TextStart = false;
    int count = 0; // ポーズ画面のためTime.deltatimeが使えない変わりの変数

    //サンプルテキスト関連ここまで



    float wid = 0;
    float hei = 0;
    private void Start()
    {
        obj = this.gameObject.GetComponent<RectTransform>();

        // 初期化用
        wid = obj.rect.width;
        hei = obj.rect.height;

        sampleMessageText.text = "";
    }
    private void Update()
    {
        if (panel.activeSelf == true)
        {
            return;
        }
        move();

        SampleText();
    }

    void move()
    {
        switch (method)
        {
            case 0:
                InputKey(List.Length, method);
                List_pos();
                sampleMessageText.text = "";
                sampleMessage[0] = "";
                break;
            case 1:
                InputKey(textspd.Length, method);
                TextSpd_posChange();
                SetSampleText(Set_textSpd);
                break;
        }
    }
    private void OnEnable()
    {
        textSpeed = 0;
        sampleMessageText.text = "";
        Listnum = 0;
        method = 0;
        this.gameObject.GetComponent<Image>().enabled = false;
        for (int i = 0; i < textspd.Length; i++)
        {
            textspd[i].GetComponent<Text>().enabled = false;
        }
        x = List[Listnum].anchoredPosition.x;
        y = List[Listnum].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = List[Listnum].rotation;
        num = Copynum;
    }
    void InputKey(int box,int method0)
    {
        switch(method0)
        {
            case 0:
                // 上入力
                if (Input.GetKeyDown(KeyCode.W) && Listnum > 0)
                {
                    Listnum--;
                }
                // 下入力
                else if (Input.GetKeyDown(KeyCode.S) && Listnum < box - 1)
                { Listnum++; }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (Listnum == 0)
                    {
                        method = 1;
                        for (int i = 0; i < textspd.Length; i++)
                        {
                            textspd[i].GetComponent<Text>().enabled = true;
                        }
                    }
                    else
                    {
                        Copynum = num;
                        Send_textSpd = Set_textSpd;
                      //  textScr.Parameter=Send_textSpd;
                        panel.SetActive(true);
                        ConfigP.GetComponent<Config>().enabled = true;
                        this.gameObject.GetComponent<C_Game>().enabled = false;
                        this.gameObject.GetComponent<Image>().enabled = false;
                        Listnum = 0;
                    }
                }
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    panel.SetActive(true);
                    ConfigP.GetComponent<Config>().enabled = true;
                    this.gameObject.GetComponent<C_Game>().enabled = false;
                    this.gameObject.GetComponent<Image>().enabled = false;
                    Listnum = 0;
                }
                break;
            case 1:
                // 上入力
                if (Input.GetKeyDown(KeyCode.W) && num > 0)
                { 
                    num--;
                }
                // 下入力
                else if (Input.GetKeyDown(KeyCode.S) && num < box - 1)
                { 
                    num++;
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {   
                    for(int i=0;i<textspd.Length;i++)
                    {
                        textspd[i].GetComponent<Text>().enabled = false;
                    }
                    method = 0;
                    textSpeed = 0;
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    for (int i = 0; i < textspd.Length; i++)
                    {
                        textspd[i].GetComponent<Text>().enabled = false;
                    }
                    method = 0;
                    Listnum = 0;
                }
                break;
        }
    }
    void List_pos()
    { 
        x = List[Listnum].anchoredPosition.x;
        y = List[Listnum].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = List[Listnum].rotation;
        obj.sizeDelta = new Vector2(wid,hei);
    }
    void TextSpd_posChange()
    {
        x = textspd[num].anchoredPosition.x;
        y = textspd[num].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = textspd[num].rotation;
        obj.sizeDelta = new Vector2(textspd[num].rect.width, textspd[num].rect.height);
        switch (num)
        {
            case 0:
                Set_textSpd = 0.08f;
                break;
            case 1:
                Set_textSpd = 0.04f;
                break;
            case 2:
                Set_textSpd = 0.02f;
                break;
        }
    }

    private void SampleText()
    {
        if (isEndMessage || sampleMessage == null)
        {
            return;
        }

        if (!isOneMessage)
        {
            if (elapsedTime >= textSpeed)
            {
                Debug.Log(textSpeed);
                sampleMessageText.text += sampleMessage[sampleNum][nowSampleNum];
                nowSampleNum++;
                elapsedTime = 0f;
                count = 0;
                if (nowSampleNum >= sampleMessage[sampleNum].Length)
                {
                    isOneMessage = true;
                }
            }
            count++;
            elapsedTime += (float)count/60.0f;
        }
        else
        {
            nowSampleNum = 0;
            sampleNum++;
            count = 0;
            //sampleMessageText.text = "";
            isOneMessage = false;
            if (sampleNum >= sampleMessage.Length)
            {
                isEndMessage = true;
            }
        }
    }
    string TestMessage="こちらはサンプルテキストです。\n速度項目の調整にお使いください";
    private void SetSampleText(float setspd)
    {
        if (setspd != textSpeed)
        {
            sampleMessageText.text = "";
            nowSampleNum = 0;
            sampleNum = 0;
            isOneMessage = false;
            isEndMessage = false;
            sampleMessage[0] = TestMessage;
            textSpeed = setspd;   
        }
    }


    /*private void OnClickedButton()
    {
        StartCoroutine(GetRequest("https://www.jma.go.jp/bosai/forecast/data/overview_forecast/230000.json"));
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // Error.
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP Error: " + webRequest.error);
                        break;
                    default:
                        // ここにはこない.
                        break;
                }
                yield break;
            }

            var response = JsonUtility.FromJson<Response>(webRequest.downloadHandler.text);
            Debug.Log("データ配信元: " + response.publishingOffice);
            Debug.Log("報告日時: " + response.reportDatetime);
            Debug.Log("対象の地域: " + response.targetArea);
            Debug.Log("ヘッドライン: " + response.headlineText);
            Debug.Log("詳細: " + response.text);
            TestMessage =response.targetArea+"\n"+response.text;
            yield return null;
        }
    }
    */
}
/*
[System.Serializable]
public class Response
{
    public string publishingOffice;
    public string reportDatetime;
    public string targetArea;
    public string headlineText;
    public string text;
}
*/


