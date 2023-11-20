using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class NameSystem : MonoBehaviour
{

    //分割文字
    [SerializeField]
    private string splitString = "<>";
    //　1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //　メッセージをすべて表示したかどうか
    private bool isEndMessage = false;

    //キャラアイコン
    private Image charaIcon;
    //表示するアイコンの名前
    [SerializeField]
    [TextArea(1, 5)]
    private string allIcon = "主人公困り";
    //分割したアイコン名
    private string[] splitIcon;
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


    void Start()
    {
        nameText = transform.GetChild(1).GetComponentInChildren<Text>();
        nameText.text = "";
        SetName(allName, allIcon);
    }

    void Update()
    {
        //messageが終わっているか、メッセージがない場合はこれ以降何もしない
        if (isEndMessage || allName == null)
        {
            return;
        }

        //１回に表示する名前を表示していない
        if (!isOneMessage)
        {
            //名前表示
            if (checkName == false)
            {
                //名前表示
                nameText.text += splitName[nameNum].Substring(nowNameNum);
                checkName = true;

                //アイコン表示
                Sprite sprite = Resources.Load<Sprite>(splitIcon[iconNum]) as Sprite;
                GameObject goImage = GameObject.Find("Icon");
                Image im = goImage.GetComponent<Image>();
                im.sprite = sprite;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                OneMessage();
            }

        }
        //１回に表示するメッセージを表示した
        else
        {
            //エンターキーを押したら次の文字表示処理
            if (Input.GetKeyDown(KeyCode.Return))
            {
                nowNameNum = 0;
                nameNum++;
                nameText.text = "";
                isOneMessage = false;
                checkName = false;

                iconNum++;

            }
        }
    }


    public void SetName(string name, string icon)
    {
        this.allName = name;
        this.allIcon = icon;
        splitName = Regex.Split(allName, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIcon = Regex.Split(allIcon, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        iconNum = 0;
        nowNameNum = 0;
        nameNum = 0;
        nameText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }

    public void SetNamePanel(string name, string icon)
    {
        SetName(name, icon);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OneMessage()
    {
        isOneMessage = true;
    }

}
