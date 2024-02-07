using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;


public class TextScript : MonoBehaviour
{
    //トークUI
    private Text messageText;

    //表示するテキスト
    [SerializeField]
    [TextArea(1, 10)]
    private string allMessage = "今回はRPGでよく使われるメッセージ表示機能を作りたいと思います。\n"
            + "メッセージが表示されるスピードの調節も可能であり、改行にも対応します。\n"
            + "改善の余地がかなりありますが、               最低限の機能は備えていると思われます。\n"
            + "ぜひ活用してみてください。";

    //使用する分割文字列
    [SerializeField] private string splitString = "<>";
    //分割したテキスト
    private string[] splitMessage;
    //分割したメッセージの何番目か
    private int messageNum;
    //テキストスピード
    [SerializeField] private float textSpeed = 0.1f;
    //経過時間
    private float elapsedTime = 0f;
    //今見ている文字番号
    private int nowTextNum = 0;
    //マウスクリックを促すアイコン
    private Image clickIcon;
    private Image clickIcon2;
    private Image clickIcon3;
    private Image clickIcon4;
    //　クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;
    //クリックアイコンの表情変化
    private int changeFace = 0;
    //クリックアイコンの点滅
    private bool changeClickIcon = false;

    //　1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //　メッセージをすべて表示したかどうか
    private bool isEndMessage = false;

    //キャラアイコン
    //キャラアイコンの画像はAssetsフォルダ内にResourceフォルダを作り、そこに入れること
    private Image charaIcon;
    //表示するアイコンの名前
    [SerializeField]
    [TextArea(1, 5)]
    //private string allIconLeft = "主人公困り";
    //private string allIconRight = "公1";
    private string allIcon;
    //分割したアイコン名
    //private string[] splitIconLeft;
    //private string[] splitIconRight;
    private int[] splitIcon;
    //アイコン配列の何番目か
    //private int iconNumL;
    //private int iconNumR;
    private int iconNum;


    //名前UI
    private Text nameText;
    //表示する名前
    [SerializeField]
    [TextArea(1, 5)]
    private string allName = "あああ<>" +
                           "いいい";
    //分割した名前
    private string[] splitName;
    //名前配列の何番目か
    private int nameNum;
    //今見ている名前番号
    private int nowNameNum = 0;
    //名前の表示したかどうか
    private bool checkName = false;

    public AudioClip sound1;
    AudioSource audioSource;

    //テキストボックスを表示中かどうか
    //false=表示してない　true=表示中
    private bool TextOnOff = false;

    private float autoTimer = 0f;
    [SerializeField] float autoTimerLimit = 2f;

    [SerializeField] private bool AutoORAanual = false;

    private string allLR = "false";
    //分割したtrueかfalse
    private bool[] splitLR;
    //左右配列の何番目か
    private int LRNum;
    //立ち絵を変更したかどうか
    private bool checkLR = false;
    //どちらの立ち絵を表示するか
    private bool LorR;
    //次の立ち絵
    private bool nextLorR;

    //最初に出す立ち絵かどうか
    private bool firstStandP = false;

    //live2Dprefab
    //キャラクター番号
    //0:一人用のみ表示用&システム用の空データ
    //1:
    //2:
    public List<GameObject> Charas = new List<GameObject>();

    [SerializeField] private GameObject CharaConection;
    [SerializeField] private Transform LeftPosT;
    [SerializeField] private GameObject LeftPos;
    [SerializeField] private Transform RightPosT;
    [SerializeField] private GameObject RightPos;
    [SerializeField] private RawImage LeftImg;
    [SerializeField] private RawImage RightImg;

    //アニメーション関連
    Animator anim;
    private string allAnims;
    private int[] splitAnims;
    private int animsNum;
    private string animStr;
    //アニメーション配列
    [SerializeField] private string[] chara1_anim;
    [SerializeField] private string[] chara2_anim;


    //CSV関連
    public TextData[] textData;

    //暗くする背景
    [SerializeField] private GameObject BackPanel;
    //Time.deltaTimeの代わり
    private float counter = 0f;

    [SerializeField] private GameObject AllTextPare;
    [SerializeField] private Text OnOffText;

    //選択肢用
    //0=非表示 1=選択肢2個 2=選択肢3個
    private int[] ChoiseTrigger;
    private int choiseNum;
    [SerializeField] private GameObject[] Choises;
    //選択肢を1回だけ表示
    private bool OneChoise = false;
    //選択肢が表示かどうか
    private bool checkChoise = false;

    public WChoiseData[] wChoiseData;
    [SerializeField] private Text[] WChoises;
    public TChoiseData[] tChoiseData;
    [SerializeField] private Text[] TChoises;

    void Start()
    {
        clickIcon = transform.Find("TextPanel/Cursor1").GetComponent<Image>();
        clickIcon.enabled = false;
        clickIcon2 = transform.Find("TextPanel/Cursor2").GetComponent<Image>();
        clickIcon2.enabled = false;
        clickIcon3 = transform.Find("TextPanel/Cursor3").GetComponent<Image>();
        clickIcon3.enabled = false;
        clickIcon4 = transform.Find("TextPanel/Cursor4").GetComponent<Image>();
        clickIcon4.enabled = false;

        messageText = transform.GetChild(2).GetComponentInChildren<Text>();
        messageText.text = "";

        audioSource = GetComponent<AudioSource>();

        nameText = transform.GetChild(3).GetComponentInChildren<Text>();
        nameText.text = "";

        OnOffText.color = Color.gray;

        AllTextPare.SetActive(false);
    }

    void Update()
    {
        //messageが終わっているか、メッセージがない、選択肢表示中の場合はこれ以降何もしない
        if (isEndMessage || splitMessage == null || checkChoise)
        {
            return;
        }

        //１回に表示するメッセージを表示していない
        if (!isOneMessage)
        {
            if (checkName == false)
            {
                //名前表示
                nameText.text += splitName[nameNum].Substring(nowNameNum);
                checkName = true;

                LorR = Convert.ToBoolean(splitLR[LRNum]);
                if (LRNum + 1 < splitLR.Length)
                {
                    nextLorR = Convert.ToBoolean(splitLR[LRNum + 1]);
                }

                //アイコン表示
                //立ち絵左
                Debug.Log(splitIcon[iconNum]);
                if (!LorR)
                {
                    Debug.Log("左立ち絵更新");
                    //live2D
                    if (firstStandP)
                    {
                        foreach(Transform child in LeftPos.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    Instantiate(Charas[splitIcon[iconNum]], LeftPosT);   //生成
                    LeftImg.color = new Color(255, 255, 255, 1);
                    RightImg.color = Color.gray;

                    //アニメーション関連
                    anim = LeftPos.transform.Find(Charas[splitIcon[iconNum]].name + "(Clone)").GetComponent<Animator>();
                    switch (splitIcon[iconNum])
                    {
                        case 0:
                            animStr = chara1_anim[splitAnims[animsNum]];
                            break;

                        case 1:
                            animStr = chara2_anim[splitAnims[animsNum]];
                            break;
                    }
                    anim.Play(animStr);

                }
                //立ち絵右
                else if (LorR)
                {
                    Debug.Log("右立ち絵更新");
                    //live2D
                    if (firstStandP)
                    {
                        foreach (Transform child in RightPos.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }
                    Instantiate(Charas[splitIcon[iconNum]], RightPosT);
                    RightImg.color = new Color(255, 255, 255, 1);
                    LeftImg.color = Color.gray;

                    //アニメーション関連
                    anim = RightPos.transform.Find(Charas[splitIcon[iconNum]].name + "(Clone)").GetComponent<Animator>();
                    switch (splitIcon[iconNum])
                    {
                        case 0:
                            animStr = chara1_anim[splitAnims[animsNum]];
                            break;

                        case 1:
                            animStr = chara2_anim[splitAnims[animsNum]];
                            break;
                    }

                    anim.Play(animStr);

                }


            }
            //テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed)
            {
                //メッセージ表示
                messageText.text += splitMessage[messageNum][nowTextNum];
                //audioSource.PlayOneShot(sound1);
                nowTextNum++;
                elapsedTime = 0f;
                counter = 0;

                //messageを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            Timer();

            //message表示中にエンターを押したら一括表示
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }

        }
        //１回に表示するメッセージを表示した
        else
        {
            Timer();

            if (AutoORAanual == false)
            {
                //クリックアイコンを点滅する時間を超えた時、反転させる
                if (elapsedTime >= clickFlashTime)
                {
                    switch (changeFace)
                    {
                        case 0:
                            clickIcon.enabled = !clickIcon.enabled;
                            break;

                        case 1:
                            clickIcon2.enabled = !clickIcon2.enabled;
                            break;

                        case 2:
                            clickIcon3.enabled = !clickIcon3.enabled;
                            break;

                        case 3:
                            clickIcon4.enabled = !clickIcon4.enabled;
                            break;
                    }

                    changeClickIcon = !changeClickIcon;

                    if (!changeClickIcon)
                    {
                        changeFace++;
                    }

                    if (changeFace >= 4)
                    {
                        changeFace = 0;
                    }

                    elapsedTime = 0f;
                    counter = 0;

                }

                OnChoise();
                //エンターキーor左クリックを押したら次の文字表示処理
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    FinishOneText();
                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        DestroyText();
                    }
                }
            }
            else
            {
                autoTimer = elapsedTime;
                clickIcon.enabled = false;
                clickIcon2.enabled = false;
                clickIcon3.enabled = false;
                clickIcon4.enabled = false;
                changeFace = 0;


                if (autoTimer >= autoTimerLimit)
                {
                    FinishOneText();

                    autoTimer = 0f;
                    counter = 0;

                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        DestroyText();
                    }
                }
            }
        }

        //スキップ
        if (Input.GetKeyDown(KeyCode.S))
        {
            Skip();
        }

        //オート・マニュアル変更
        if (Input.GetKeyDown(KeyCode.M))
        {
            AutoORAanual = !AutoORAanual;
            if (!AutoORAanual)
            {
                OnOffText.text = "OFF";
                OnOffText.color = Color.gray;
            }
            else
            {
                OnOffText.text = "ON";
                OnOffText.color = Color.white;
            }
            Debug.Log(AutoORAanual);
        }



    }

    //次の文字表示処理
    private void FinishOneText()
    {
        nowTextNum = 0;
        messageNum++;
        messageText.text = "";
        clickIcon.enabled = false;
        clickIcon2.enabled = false;
        clickIcon3.enabled = false;
        clickIcon4.enabled = false;
        changeFace = 0;
        changeClickIcon = false;
        elapsedTime = 0f;
        isOneMessage = false;

        nowNameNum = 0;
        nameNum++;
        nameText.text = "";
        checkName = false;

        iconNum++;

        LRNum++;

        animsNum++;

        firstStandP = true;

        choiseNum++;
    }

    //全てのメッセージを表示されたら
    private void DestroyText()
    {
        foreach (Transform child in LeftPos.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in RightPos.transform)
        {
            Destroy(child.gameObject);
        }

        isEndMessage = true;
        TextOnOff = false;
        //transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        CharaConection.SetActive(false);
        BackPanel.SetActive(false);
        Time.timeScale = 1;
        AllTextPare.SetActive(false);
    }

    //時間計測用(TimeScaleの影響でdeltaTimeが使えないため)
    private void Timer()
    {
        counter++;
        elapsedTime = counter / 60f;
    }

    //選択肢表示
    private void OnChoise()
    {
        //選択肢表示
        if (ChoiseTrigger[choiseNum] > 0 && !OneChoise)
        {
            switch (ChoiseTrigger[choiseNum])
            {
                case 1:
                    Choises[ChoiseTrigger[choiseNum]].SetActive(true);
                    break;
                case 2:
                    Choises[ChoiseTrigger[choiseNum]].SetActive(true);
                    break;
            }
            OneChoise = true;
            checkChoise = true;
        }

    }

    void SetText(string message, string name, string icon, string iconLorR, string anims)
    {
        //this.allMessage = message;
        //this.allName = name;
        ////this.allIconLeft = iconLeft;
        ////this.allIconRight = iconRight;
        //this.allIcon = icon;
        //this.allLR = iconLorR;
        //this.allAnims = anims;
        ////分割文字列で一回に表示するメッセージを分割する
        //splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        //splitName = Regex.Split(allName, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        ////splitIconLeft = Regex.Split(allIconLeft, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        ////splitIconRight = Regex.Split(allIconRight, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        //splitIcon= Regex.Split(allIcon, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        //splitLR = Regex.Split(allLR, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        //splitAnims= Regex.Split(allAnims, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);

        //nowTextNum = 0;
        //messageNum = 0;
        //messageText.text = "";
        ////iconNumL = 0;
        ////iconNumR = 0;
        //iconNum = 0;
        //nowNameNum = 0;
        //nameNum = 0;
        //nameText.text = "";
        //isOneMessage = false;
        //isEndMessage = false;
        //TextOnOff = true;

        //LRNum = 0;

        //animsNum = 0;

        //firstStandP = false;
    }

    //他のスクリプトから新しいメッセージを設定し、UIをアクティブにする
    public void SetTextPanel(string message, string name, string icon, string iconLorR, string anims)
    {
        //SetText(message, name, icon, iconLorR, anims);
        ////transform.GetChild(0).gameObject.SetActive(true);
        ////transform.GetChild(1).gameObject.SetActive(true);
        //transform.GetChild(2).gameObject.SetActive(true);
        //transform.GetChild(3).gameObject.SetActive(true);
        //CharaConection.SetActive(true);
        //Time.timeScale = 0;
    }

    public bool CheckTextOnOff()
    {
        return TextOnOff;
    }

    public void ChangeCheckChoise()
    {
        checkChoise = false;
        Skip();
    }


    public float Parameter
    {
        set
        {
            textSpeed = value;
        }
    }


    //csvファイルのセット
    public void SetCSVFile(string csvFiles, string csvChoise)
    {
        TextAsset textAsset = new TextAsset();
        textAsset = Resources.Load(csvFiles, typeof(TextAsset)) as TextAsset;
        textData = CSVSerializer.Deserialize<TextData>(textAsset.text);

        splitMessage = new string[textData.Length];
        splitName = new string[textData.Length];
        splitIcon = new int[textData.Length];
        splitLR = new bool[textData.Length];
        splitAnims = new int[textData.Length];
        ChoiseTrigger = new int[textData.Length];

        for (int i = 0; i < textData.Length; i++)
        {
            this.splitMessage[i] = textData[i].message;
            this.splitName[i] = textData[i].charaName;
            this.splitIcon[i] = textData[i].charaIcon;
            this.splitLR[i] = textData[i].LorR;
            this.splitAnims[i] = textData[i].anims;
            this.ChoiseTrigger[i] = textData[i].choiseNo;
            Debug.Log("読み込み");
        }

        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        iconNum = 0;
        nowNameNum = 0;
        nameNum = 0;
        nameText.text = "";
        isOneMessage = false;
        isEndMessage = false;
        TextOnOff = true;

        LRNum = 0;

        animsNum = 0;

        firstStandP = false;

        choiseNum = 0;
        OneChoise = false;
        checkChoise = false;

        //選択肢があるかどうか
        for(int j = 0; j < textData.Length; j++)
        {
            //選択肢があった場合
            if(ChoiseTrigger[j] != 0)
            {
                //選択肢のテキストをセット
                switch (ChoiseTrigger[j])
                {
                    case 1:
                        TextAsset wChoiseAsset = new TextAsset();
                        wChoiseAsset = Resources.Load(csvChoise, typeof(TextAsset)) as TextAsset;
                        wChoiseData = CSVSerializer.Deserialize<WChoiseData>(wChoiseAsset.text);

                        WChoises[0].text = wChoiseData[0].UpText;
                        WChoises[1].text = wChoiseData[0].DownText;
                        break;

                    case 2:
                        TextAsset tChoiseAsset = new TextAsset();
                        tChoiseAsset = Resources.Load(csvChoise, typeof(TextAsset)) as TextAsset;
                        tChoiseData = CSVSerializer.Deserialize<TChoiseData>(tChoiseAsset.text);

                        TChoises[0].text = tChoiseData[0].UpText;
                        TChoises[1].text = tChoiseData[0].CenterText;
                        TChoises[2].text = tChoiseData[0].DownText;
                        break;
                }
            }
        }
    }

    //AllTextsから呼びだす
    public void SetCSVPanel(string csvFiles, string csvChoise)
    {
        SetCSVFile(csvFiles, csvChoise);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        CharaConection.SetActive(true);
        BackPanel.SetActive(true);
        Time.timeScale = 0;

    }

    //スキップ処理
    public void Skip()
    {
        FinishOneText();
        DestroyText();
    }


}



//テキスト
[System.Serializable]
public class TextData
{
    public string message;
    public string charaName;
    public int charaIcon;
    public bool LorR;
    public int anims;
    public int choiseNo;
}

//選択肢2個用
[System.Serializable]
public class WChoiseData
{
    public string UpText;
    public string DownText;
}

//選択肢3個用
[System.Serializable]
public class TChoiseData
{
    public string UpText;
    public string CenterText;
    public string DownText;
}