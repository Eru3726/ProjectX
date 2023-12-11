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
    private string allIconLeft = "主人公困り";
    private string allIconRight = "公1";
    //分割したアイコン名
    private string[] splitIconLeft;
    private string[] splitIconRight;
    //アイコン配列の何番目か
    private int iconNumL;
    private int iconNumR;


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
    private string[] splitLR;
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
    //0:
    //1:
    //2:
    public List<GameObject> Charas = new List<GameObject>();

    [SerializeField] private GameObject CharaConection;
    [SerializeField] private Transform LeftPos;
    [SerializeField] private Transform RightPos;
    [SerializeField] private RawImage LeftImg;
    [SerializeField] private RawImage RightImg;

    // Start is called before the first frame update
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
        SetText(allMessage, allName, allIconLeft,allIconRight,allLR);


    }

    // Update is called once per frame
    void Update()
    {
        //messageが終わっているか、メッセージがない場合はこれ以降何もしない
        if (isEndMessage || allMessage == null)
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
                    //画像
                    //Sprite sprite1 = Resources.Load<Sprite>(splitIconLeft[iconNumL]) as Sprite;
                    //GameObject goImageLeft = GameObject.Find("Icon1");
                    //Image im1 = goImageLeft.GetComponent<Image>();
                    //im1.sprite = sprite1;

                    //live2D
                    Instantiate(Charas[int.Parse(splitIconLeft[iconNumL])], LeftPos);   //生成
                    LeftImg.color = new Color(255, 255, 255);
                    RightImg.color = new Color(140, 140, 140);


                }
                //立ち絵右
                else if (LorR)
                {
                    Debug.Log("右立ち絵更新");
                    //画像
                    //Sprite sprite2 = Resources.Load<Sprite>(splitIconRight[iconNumR]) as Sprite;
                    //GameObject goImageRight = GameObject.Find("Icon2");
                    //Image im2 = goImageRight.GetComponent<Image>();
                    //im2.sprite = sprite2;

                    //live2D
                    Instantiate(Charas[int.Parse(splitIconRight[iconNumR])], RightPos);
                    RightImg.color = new Color(255, 255, 255);
                    LeftImg.color = new Color(140, 140, 140);


                }

            }
            //テキスト表示時間を経過したらメッセージを追加
            if (elapsedTime >= textSpeed)
            {
                //メッセージ表示
                messageText.text += splitMessage[messageNum][nowTextNum];
                audioSource.PlayOneShot(sound1);
                nowTextNum++;
                elapsedTime = 0f;

                //messageを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;


            ////message表示中にエンターを押したら一括表示
            //if (Input.GetKeyDown(KeyCode.Return))
            //{
            //    messageText.text += splitMessage[messageNum].Substring(nowTextNum);
            //    isOneMessage = true;
            //}

        }
        //１回に表示するメッセージを表示した
        else
        {
            if (AutoORAanual == false)
            {
                elapsedTime += Time.deltaTime;

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

                }

                //エンターキーor左クリックを押したら次の文字表示処理
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
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
                    isOneMessage = false;
                    checkName = false;

                    if (!nextLorR)
                    {
                        if (firstStandP)
                        {
                            iconNumL++;
                        }
                    }
                    else if (nextLorR)
                    {
                        if (firstStandP)
                        {
                            iconNumR++;
                        }
                    }
                    LRNum++;

                    firstStandP = true;

                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        TextOnOff = false;
                        //transform.GetChild(0).gameObject.SetActive(false);
                        //transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        CharaConection.SetActive(false);
                        Time.timeScale = 1;
                    }
                }
            }
            else
            {
                autoTimer += Time.deltaTime;
                clickIcon.enabled = false;
                clickIcon2.enabled = false;
                clickIcon3.enabled = false;
                clickIcon4.enabled = false;
                changeFace = 0;


                if (autoTimer >= autoTimerLimit)
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    clickIcon2.enabled = false;
                    clickIcon3.enabled = false;
                    clickIcon4.enabled = false;
                    changeFace = 0;
                    elapsedTime = 0f;
                    isOneMessage = false;

                    nowNameNum = 0;
                    nameNum++;
                    nameText.text = "";
                    isOneMessage = false;
                    checkName = false;

                    if (!nextLorR)
                    {
                        if (firstStandP)
                        {
                            iconNumL++;
                        }
                    }
                    else if (nextLorR)
                    {
                        if (firstStandP)
                        {
                            iconNumR++;
                        }
                    }
                    LRNum++;

                    firstStandP = true;

                    autoTimer = 0f;

                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        TextOnOff = false;
                        //transform.GetChild(0).gameObject.SetActive(false);
                        //transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        CharaConection.SetActive(false);
                        Time.timeScale = 1;
                    }
                }
            }
        }
    }

    void SetText(string message, string name, string iconLeft, string iconRight, string iconLorR)
    {
        this.allMessage = message;
        this.allName = name;
        this.allIconLeft = iconLeft;
        this.allIconRight = iconRight;
        this.allLR = iconLorR;
        //分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitName = Regex.Split(allName, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIconLeft = Regex.Split(allIconLeft, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIconRight = Regex.Split(allIconRight, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitLR = Regex.Split(allLR, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        iconNumL = 0;
        iconNumR = 0;
        nowNameNum = 0;
        nameNum = 0;
        nameText.text = "";
        isOneMessage = false;
        isEndMessage = false;
        TextOnOff = true;

        LRNum = 0;

        firstStandP = false;
    }

    //他のスクリプトから新しいメッセージを設定し、UIをアクティブにする
    public void SetTextPanel(string message, string name, string iconLeft, string iconRight, string iconLorR)
    {
        SetText(message, name, iconLeft, iconRight, iconLorR);
        //transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        CharaConection.SetActive(true);
        Time.timeScale = 0;
    }

    public bool CheckTextOnOff()
    {
        return TextOnOff;
    }


    public float Parameter
    {
        set
        {
            textSpeed = value;
        }
    }

}
