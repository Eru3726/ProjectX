using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


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
    //　クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;
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
    //名前配列の何番目か
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

    // Start is called before the first frame update
    void Start()
    {
        clickIcon = transform.Find("TextPanel/Cursor").GetComponent<Image>();
        clickIcon.enabled = false;
        messageText = transform.GetChild(2).GetComponentInChildren<Text>();
        messageText.text = "";

        audioSource = GetComponent<AudioSource>();

        nameText = transform.GetChild(3).GetComponentInChildren<Text>();
        nameText.text = "";
        SetText(allMessage, allName, allIconLeft,allIconRight);


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

                //アイコン表示
                Sprite sprite1 = Resources.Load<Sprite>(splitIconLeft[iconNum]) as Sprite;
                Sprite sprite2 = Resources.Load<Sprite>(splitIconRight[iconNum]) as Sprite;
                GameObject goImageLeft = GameObject.Find("Icon1");
                GameObject goImageRight = GameObject.Find("Icon2");
                Image im1 = goImageLeft.GetComponent<Image>();
                Image im2 = goImageRight.GetComponent<Image>();
                im1.sprite = sprite1;
                im2.sprite = sprite2;
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
                    clickIcon.enabled = !clickIcon.enabled;
                    elapsedTime = 0f;
                }

                //エンターキーor左クリックを押したら次の文字表示処理
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;

                    nowNameNum = 0;
                    nameNum++;
                    nameText.text = "";
                    isOneMessage = false;
                    checkName = false;

                    iconNum++;

                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        TextOnOff = false;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        Time.timeScale = 1;
                    }
                }
            }
            else
            {
                autoTimer += Time.deltaTime;
                clickIcon.enabled = false;

                if (autoTimer >= autoTimerLimit)
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;

                    nowNameNum = 0;
                    nameNum++;
                    nameText.text = "";
                    isOneMessage = false;
                    checkName = false;

                    iconNum++;

                    autoTimer = 0f;

                    //messageがすべて表示されていたらゲームオブジェクト自体の削除
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        TextOnOff = false;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(3).gameObject.SetActive(false);
                        Time.timeScale = 1;
                    }
                }
            }
        }
    }

    void SetText(string message, string name, string iconLeft, string iconRight)
    {
        this.allMessage = message;
        this.allName = name;
        this.allIconLeft = iconLeft;
        this.allIconRight = iconRight;
        //分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitName = Regex.Split(allName, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIconLeft = Regex.Split(allIconLeft, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIconRight = Regex.Split(allIconRight, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
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
    }

    //他のスクリプトから新しいメッセージを設定し、UIをアクティブにする
    public void SetTextPanel(string message, string name, string iconLeft, string iconRight)
    {
        SetText(message, name, iconLeft, iconRight);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public bool CheckTextOnOff()
    {
        return TextOnOff;
    }
}
