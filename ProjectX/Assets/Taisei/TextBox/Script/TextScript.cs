using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Text;


public class TextScript : MonoBehaviour
{
    [SerializeField] ItemDataBase iData;
    private int[] splitItemFrg;
    private int itemFrgNum = 0;

    //トークUI
    private Text messageText;

    //表示するテキスト
    //[SerializeField]
    //[TextArea(1, 10)]
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
    //private string allIconLeft = "主人公困り";
    //private string allIconRight = "公1";
    //[SerializeField]
    //[TextArea(1, 5)]
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
    //[SerializeField]
    //[TextArea(1, 5)]
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
    
    //自動再生か手動再生か
    private bool AutoORAanual = false;
    //早送り中かどうか
    private bool FastORNormal = false;

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
    //1:主人公
    //2:ムカムカ
    //3:メソメソ
    //4:ドーラ
    //5:マスター
    //6:グレン
    //7:エリス
    public List<GameObject> Charas = new List<GameObject>();

    [SerializeField] private GameObject CharaConection;
    [SerializeField] private Transform LeftPosT;
    [SerializeField] private GameObject LeftPos;
    [SerializeField] private Transform RightPosT;
    [SerializeField] private GameObject RightPos;
    [SerializeField] private RawImage LeftImg;
    [SerializeField] private RawImage RightImg;

    private string checkChildNameL;
    private string checkChildNameR;

    //アニメーション関連
    Animator animL;
    Animator animR;
    private string allAnims;
    private int[] splitAnims;
    private int animsNum;
    private string animStr;
    private string saveAnim;
    //アニメーション配列
    [SerializeField] private string[] chara1_anim;  //主人公
    [SerializeField] private string[] chara2_anim;  //ムカムカ
    [SerializeField] private string[] chara3_anim;  //メソメソ
    [SerializeField] private string[] chara4_anim;  //ドーラ
    [SerializeField] private string[] chara5_anim;  //マスター
    [SerializeField] private string[] chara6_anim;  //グレン
    [SerializeField] private string[] chara7_anim;  //エリス
        
    //CSV関連
    public TextData[] textData;

    //暗くする背景
    [SerializeField] private GameObject BackPanel;
    //Time.deltaTimeの代わり
    private float counter = 0f;

    [SerializeField] private GameObject AllTextPare;
    [SerializeField] private Text OnOffText;
    [SerializeField] private Text OnOffFastText;

    //選択肢用
    //0=非表示 1=選択肢2個 2=選択肢3個
    private int[] ChoiseTrigger;
    private int choiseNum;
    //選択肢配列
    //0=空(選択肢無し) 1=選択肢2個バージョン 2=選択肢3個バージョン
    [SerializeField] private GameObject[] Choises;
    //選択肢を1回だけ表示
    private bool OneChoise = false;
    //選択肢が表示かどうか
    private bool checkChoise = false;
    //選択肢フラグ
    private int choiseFlg;
    //選択肢があるかどうか
    private bool choiseYN = false;

    //使用する選択肢シート
    private string[] ChoiseName;
    private int cNameNum = 0;

    public WChoiseData[] wChoiseData;
    [SerializeField] private Text[] WChoises;
    public TChoiseData[] tChoiseData;
    [SerializeField] private Text[] TChoises;

    //選択肢で決定を押したかどうか
    private bool EnterCheck = false;
    //選択肢で選んだやつを外部が参照するようのやつ
    private int checkRouteFlg;
    
    //次の文章を表示するとき
    //0=3つ先の文章に飛ばす 1=2つ先の文章に飛ばす 2=1つ先の文章に飛ばす 
    private int routeFlg = 2;
    private bool routeOnOFF = false;
    private int routeIdx = 0;

    //スキップ処理関連
    private Choise choiseW; //選択肢2個バージョン
    private Choise choiseT; //選択肢3個バージョン
    private Choise choiseSkip;  //スキップ時用
    //スキップする場所の番号
    private int[] skipPoint;
    private int skipCount = 0;
    private int skipNum = 0;
    //スキップ時に選択肢があるかどうか
    private bool skipFlg = false;
    //スキップ時のテキスト表示用;
    public SkipChoiseData[] sChoiseData;
    [SerializeField] private GameObject SkipTextObj;
    private Text skipText;
    private string[] splitSkipText = { "スキップしますか？" };
    private int nowSkipNo = 0;
    private float skipTextTime = 0f;
    private bool skipMessage = false;
    private bool skipFinishMessage = false;
    [SerializeField] private GameObject SkipChoise;
    [SerializeField] private Text[] SkipChoiseText;
    private bool skipOnOff = false;

    //テキスト配列の現在地確認用
    private int nowPoint = 0;

    //テキストログ関連
    [SerializeField] private GameObject LogObj;
    //ログを表示しているかどうか
    public bool logCheck = false;
    //ログ出力先テキスト
    [SerializeField] private Text logMessage;
    //データ
    private List<string> allLogs;
    //ログを保存する数
    [SerializeField] private int allLogDataNum = 10;
    //縦のスクロールバー
    [SerializeField] private Scrollbar verticalScrollbar;
    private StringBuilder logTextStringBuilder;

    void Start()
    {
        skipText = transform.GetChild(4).GetComponentInChildren<Text>();
        skipText.text = "";

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
        OnOffFastText.color = Color.gray;

        choiseW = Choises[1].GetComponent<Choise>();
        choiseT = Choises[2].GetComponent<Choise>();
        choiseSkip = SkipChoise.GetComponent<Choise>();

        allLogs = new List<string>();
        logTextStringBuilder = new StringBuilder();

        LogObj.SetActive(false);
        SkipTextObj.SetActive(false);
        AllTextPare.SetActive(false);
    }

    void Update()
    {
        if (!skipOnOff)
        {
            TextMain();
            //ログ表示
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (!logCheck)
                {
                    LogObj.SetActive(true);
                }
                else
                {
                    LogObj.SetActive(false);
                }
                logCheck = !logCheck;
            }
        }
        else
        {
            SkipText();
        }
    }

    private void TextMain()
    {
        //messageが終わっているか、メッセージがない、選択肢表示中の場合はこれ以降何もしない
        if (isEndMessage || splitMessage == null || checkChoise || logCheck)
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
                if (!LorR)
                {
                    Debug.Log("左立ち絵更新");
                    LeftImg.color = new Color(255, 255, 255, 1);
                    RightImg.color = Color.gray;
                    //キャラの立ち絵を表示させる場合
                    if (splitIcon[iconNum] != 0)
                    {
                        //今表示しているキャラと次表示するキャラが違ったらor何も生成されてなかったら
                        if (LeftPos.transform.childCount == 0 
                            || !checkChildNameL.Contains(Charas[splitIcon[iconNum]].name))
                        {
                            //live2D
                            if (firstStandP)
                            {
                                foreach (Transform child in LeftPos.transform)
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                            //アニメーション関連
                            //システム以外の場合
                            if (splitIcon[iconNum] != 0)
                            {
                                Instantiate(Charas[splitIcon[iconNum]], LeftPosT);   //生成
                                checkChildNameL = Charas[splitIcon[iconNum]].name;
                            }
                            animL = LeftPosT.Find(Charas[splitIcon[iconNum]].name + "(Clone)").GetComponent<Animator>();
                        }
                        CharaAnim();
                        if (animStr != saveAnim || !firstStandP)
                        {
                            animL.Play(animStr);
                        }
                        saveAnim = animStr;
                    }
                    //システムメッセージの場合
                    else
                    {
                        foreach (Transform child in LeftPos.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }
                }
                //立ち絵右
                else if (LorR)
                {
                    Debug.Log("右立ち絵更新");
                    RightImg.color = new Color(255, 255, 255, 1);
                    LeftImg.color = Color.gray;
                    //キャラの立ち絵を表示させる場合
                    if (splitIcon[iconNum] != 0)
                    {
                        //今表示しているキャラと次表示するキャラが違ったらor何も生成されてなかったら
                        if (RightPos.transform.childCount == 0
                            || !checkChildNameR.Contains(Charas[splitIcon[iconNum]].name))
                        {
                            //live2D
                            if (firstStandP)
                            {
                                foreach (Transform child in RightPos.transform)
                                {
                                    Destroy(child.gameObject);
                                }
                            }

                            //アニメーション関連
                            //システム以外の場合
                            if (splitIcon[iconNum] != 0)
                            {
                                Instantiate(Charas[splitIcon[iconNum]], RightPosT);
                                checkChildNameR = Charas[splitIcon[iconNum]].name;
                            }
                            animR = RightPosT.Find(Charas[splitIcon[iconNum]].name + "(Clone)").GetComponent<Animator>();
                        }
                        CharaAnim();
                        if (animStr != saveAnim || !firstStandP)
                        {
                            animR.Play(animStr);
                        }
                        saveAnim = animStr;
                    }
                    //システムメッセージの場合
                    else
                    {
                        foreach (Transform child in RightPos.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }
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
                    OnChoise();
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

                //エンターキーor左クリックを押したら次の文字表示処理
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0) || EnterCheck)
                {
                    ItemFlgOn();
                    FinishOneText();
                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        Debug.Log("テキスト終了");
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

                //設定した時間を超えたら次の文字表示処理
                if (autoTimer >= autoTimerLimit || EnterCheck)
                {
                    FinishOneText();

                    autoTimer = 0f;
                    counter = 0;

                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        Debug.Log("テキスト終了");
                        DestroyText();
                    }
                }
            }
        }
        //スキップ
        if (Input.GetKeyDown(KeyCode.S))
        {
            skipOnOff = true;
            SkipTextObj.SetActive(true);
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
        }

        //早送り
        if (Input.GetKeyDown(KeyCode.N))
        {
            FastORNormal = !FastORNormal;
            if (!FastORNormal)
            {
                OnOffFastText.text = "OFF";
                OnOffFastText.color = Color.gray;
                textSpeed *= 2;
                autoTimerLimit *= 2;
            }
            else
            {
                OnOffFastText.text = "ON";
                OnOffFastText.color = Color.white;
                textSpeed /= 2;
                autoTimerLimit /= 2;
            }
        }
    }

    //次の文字表示処理
    private void FinishOneText()
    {
        if (messageNum < splitMessage.Length)
        {
            //テキストログの追加
            AddLogText();
            if (ChoiseTrigger[choiseNum] != 0)
            {
                AddLogChoise();
            }

            //選択肢直後
            if (routeOnOFF)
            {
                if (ChoiseTrigger[choiseNum] == 999)
                {
                    //テキスト終了
                    DestroyText();
                }

                //分岐後の処理
                switch (routeFlg)
                {
                    case 0:
                        PlusThree();
                        break;

                    case 1:
                        PlusTwo();
                        break;

                    case 2:
                        PlusOne();
                        break;
                }
                routeOnOFF = false;
                routeFlg = 2;
            }
            //選択肢直後以外
            else
            {
                //選択肢による分岐
                switch (ChoiseTrigger[choiseNum])
                {
                    case 0:
                        PlusOne();
                        routeFlg = 2;
                        break;

                    case 1:
                        if (choiseW.ChoiseFlg() == 0)
                        {
                            PlusOne();

                            routeFlg = 1;
                            routeOnOFF = true;
                        }
                        else
                        {
                            PlusTwo();

                            routeFlg = 2;
                            routeOnOFF = true;
                        }
                        break;

                    case 2:
                        if (choiseT.ChoiseFlg() == 0)
                        {
                            PlusOne();

                            routeFlg = 0;
                            routeOnOFF = true;

                        }
                        else if (choiseT.ChoiseFlg() == 1)
                        {
                            PlusTwo();

                            routeFlg = 1;
                            routeOnOFF = true;
                        }
                        else
                        {
                            PlusThree();

                            routeFlg = 2;
                            routeOnOFF = true;
                        }
                        break;
                }
            }
        }

        nowTextNum = 0;
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
        nameText.text = "";
        checkName = false;

        firstStandP = true;
        OneChoise = false;
        EnterCheck = false;
    }

    private void PlusOne()
    {
        messageNum++;
        nameNum++;
        iconNum++;
        LRNum++;
        animsNum++;
        choiseNum++;
        cNameNum++;
        skipNum++;
        nowPoint++;
        itemFrgNum++;

        routeIdx = 1;
    }

    private void PlusTwo()
    {
        messageNum += 2;
        nameNum += 2;
        iconNum += 2;
        LRNum += 2;
        animsNum += 2;
        choiseNum += 2;
        cNameNum += 2;
        skipNum += 2;
        nowPoint += 2;
        itemFrgNum += 2;

        routeIdx = 2;
    }

    private void PlusThree()
    {
        messageNum += 3;
        nameNum += 3;
        iconNum += 3;
        LRNum += 3;
        animsNum += 3;
        choiseNum += 3;
        cNameNum += 3;
        skipNum += 3;
        nowPoint += 3;
        itemFrgNum += 3;

        routeIdx = 3;
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

        //選択肢箇所があった場合
        if (skipPoint != null && skipPoint.Length != 0)
        {
            Debug.Log("配列リセット");
            //選択肢表示場所を保存した配列をリセット
            Array.Clear(skipPoint, 0, skipCount);
        }

        routeFlg = 2;
        routeIdx = 0;
        routeOnOFF = false;
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
        if (ChoiseTrigger[choiseNum] != 0 && !OneChoise)
        {
            switch (ChoiseTrigger[choiseNum])
            {
                case 1:
                    TextAsset wChoiseAsset = new TextAsset();
                    wChoiseAsset = Resources.Load(ChoiseName[cNameNum], typeof(TextAsset)) as TextAsset;
                    wChoiseData = CSVSerializer.Deserialize<WChoiseData>(wChoiseAsset.text);

                    WChoises[0].text = wChoiseData[0].UpText;
                    WChoises[1].text = wChoiseData[0].DownText;

                    Choises[ChoiseTrigger[choiseNum]].SetActive(true);
                    break;

                case 2:
                    TextAsset tChoiseAsset = new TextAsset();
                    tChoiseAsset= Resources.Load(ChoiseName[cNameNum], typeof(TextAsset)) as TextAsset;
                    tChoiseData = CSVSerializer.Deserialize<TChoiseData>(tChoiseAsset.text);

                    TChoises[0].text = tChoiseData[0].UpText;
                    TChoises[1].text = tChoiseData[0].CenterText;
                    TChoises[2].text = tChoiseData[0].DownText;

                    Choises[ChoiseTrigger[choiseNum]].SetActive(true);
                    break;
            }
            OneChoise = true;
            checkChoise = true;
        }

    }

    //キャラのアニメーション
    private void CharaAnim()
    {
        switch (splitIcon[iconNum])
        {
            //主人公
            case 1:
                animStr = chara1_anim[splitAnims[animsNum]];
                break;

            //ムカムカ
            case 2:
                animStr = chara2_anim[splitAnims[animsNum]];
                break;

            //メソメソ
            case 3:
                animStr = chara3_anim[splitAnims[animsNum]];
                break;
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

    //テキストが終わったかどうか
    public bool CheckTextOnOff()
    {
        return TextOnOff;
    }

    //選んだ選択肢を外部で参照するやつ
    public int CheckRouteFlg()
    {
        return checkRouteFlg;
    }

    //選択肢を選んで決定を押したとき
    public void ChangeCheckChoise()
    {
        checkChoise = false;
        EnterCheck = true;
    }

    public float Parameter
    {
        set
        {
            textSpeed = value;
            Debug.Log(textSpeed);
        }
    }


    //csvファイルのセット
    public void SetCSVFile(string csvFiles)
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
        ChoiseName = new string[textData.Length];
        splitItemFrg = new int[textData.Length];

        skipPoint = new int[textData.Length];

        for (int i = 0; i < textData.Length; i++)
        {
            this.splitMessage[i] = textData[i].message;
            this.splitName[i] = textData[i].charaName;
            this.splitIcon[i] = textData[i].charaIcon;
            this.splitLR[i] = textData[i].LorR;
            this.splitAnims[i] = textData[i].anims;
            this.ChoiseTrigger[i] = textData[i].choiseNo;
            this.ChoiseName[i] = textData[i].choiseName;
            this.splitItemFrg[i] = textData[i].ItemFrg;
            Debug.Log("読み込み");

            //選択肢があった場合
            if (ChoiseTrigger[i] != 0)
            {
                Debug.Log("選択肢発見");
                skipPoint[i] = i;
            }
            else
            {
                skipPoint[i] = 0;
            }
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
        cNameNum = 0;
        nowPoint = 0;
        skipNum = 0;
        itemFrgNum = 0;
    }

    //AllTextsから呼びだす
    public void SetCSVPanel(string csvFiles)
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        CharaConection.SetActive(true);
        BackPanel.SetActive(true);
        Time.timeScale = 0;
        SetCSVFile(csvFiles);
    }

    //スキップ処理
    public void Skip()
    {
        FinishOneText();
        DestroyText();
    }

    private void SkipText()
    {
        SkipTextObj.SetActive(true);
        if (!skipMessage)
        {
            if (skipTextTime >= textSpeed)
            {
                skipText.text += splitSkipText[0][nowSkipNo];
                nowSkipNo++;
                skipTextTime = 0f;
                counter = 0;
                elapsedTime = 0f;
                if (nowSkipNo >= splitSkipText[0].Length)
                {
                    TextAsset skipChoiseAsset = new TextAsset();
                    skipChoiseAsset = Resources.Load("testChoiseW", typeof(TextAsset)) as TextAsset;
                    sChoiseData = CSVSerializer.Deserialize<SkipChoiseData>(skipChoiseAsset.text);

                    SkipChoiseText[0].text = sChoiseData[0].UpText;
                    SkipChoiseText[1].text = sChoiseData[0].DownText;

                    skipMessage = true;
                    checkChoise = true;
                    SkipChoise.SetActive(true);
                }
            }
            Timer();
            skipTextTime = elapsedTime;
        }
        else
        {
            if (!checkChoise)
            {
                skipFinishMessage = true;
                choiseFlg = choiseSkip.ChoiseFlg();
                if (choiseFlg == 0)
                {
                    SkipTextObj.SetActive(false);
                    SkipReset();
                    SkipCheck();
                }
                else
                {
                    SkipTextObj.SetActive(false);
                    SkipReset();
                }
            }
        }
    }

    private void SkipCheck()
    {
        //選択肢があるかどうか
        for(int i = nowPoint; i < textData.Length; i++)
        {
            itemFrgNum = i;
            ItemFlgOn();
            if (ChoiseTrigger[i] != 0)
            {
                choiseYN = true;
                skipNum = i;
                break;
            }
        }
        if (!choiseYN)
        {
            //選択肢がない場合
            Skip();
        }
        else
        {
            //選択肢がある場合
            nowTextNum = 0;
            messageNum = skipPoint[skipNum];
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
            nameNum = skipPoint[skipNum];
            nameText.text = "";
            checkName = false;
            iconNum = skipPoint[skipNum];
            LRNum = skipPoint[skipNum];
            animsNum = skipPoint[skipNum];
            firstStandP = true;
            choiseNum = skipPoint[skipNum];
            cNameNum = skipPoint[skipNum];
            itemFrgNum = skipPoint[skipNum];

            if (AutoORAanual)
            {
                autoTimer = 0f;
                counter = 0;
            }
            choiseYN = false;
            nowPoint = skipPoint[skipNum];
            routeOnOFF = false;
        }
    }

    private void SkipReset()
    {
        Debug.Log("スキップ処理をリセットします");
        skipText.text = "";
        nowSkipNo = 0;
        counter = 0;
        elapsedTime = 0f;
        skipMessage = false;
        skipFinishMessage = false;
        skipOnOff = false;
    }

    //ログテキストの追加
    public void AddLogText()
    {
        allLogs.Add("・" + splitName[nameNum] + "\n" + "『" + splitMessage[messageNum] + "』");
        //ログの最大保存数を超えたら古いログを削除
        if (allLogs.Count > allLogDataNum)
        {
            allLogs.RemoveRange(0, allLogs.Count - allLogDataNum);
        }
        //ログテキストの表示
        ViewLogText();
    }

    //選択肢の内容をログに追加
    public void AddLogChoise()
    {
        if(ChoiseTrigger[choiseNum]== 1)
        {
            allLogs.Add("<color=#EA0F0F>Select\n" + WChoises[choiseW.ChoiseFlg()].text + "</color>");
        }
        else if (ChoiseTrigger[choiseNum] == 2)
        {
            allLogs.Add("<color=#EA0F0F>Select\n" + TChoises[choiseT.ChoiseFlg()].text + "</color>");
        }
        //ログの最大保存数を超えたら古いログを削除
        if (allLogs.Count > allLogDataNum)
        {
            allLogs.RemoveRange(0, allLogs.Count - allLogDataNum);
        }
        //ログテキストの表示
        ViewLogText();
    }

    //ログテキストの表示
    public void ViewLogText()
    {
        logTextStringBuilder.Clear();
        List<string> selectedLogs = new List<string>();
        selectedLogs = allLogs;
        foreach(var log in selectedLogs)
        {
            logTextStringBuilder.Append(Environment.NewLine + log);
        }
        logMessage.text = logTextStringBuilder.ToString().TrimEnd();
        UpdataScrollBar();
    }

    //スクロールバーの位置を更新
    public void UpdataScrollBar()
    {
        verticalScrollbar.value = 0f;
    }

    //アイテムフラグ関連
    public void ItemFlgOn()
    {
        if (splitItemFrg[itemFrgNum] != 999)
        {
            iData.ItemDATA[splitItemFrg[itemFrgNum]].getFlag = true;
        }
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
    public string choiseName;
    public int ItemFrg;
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

//スキップ用選択肢
[System.Serializable]
public class SkipChoiseData
{
    public string UpText;
    public string DownText;
}