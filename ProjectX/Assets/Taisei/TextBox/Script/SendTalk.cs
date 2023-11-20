using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTalk : MonoBehaviour
{
    /*以下の内容があればメッセージを送ることができる*/

    //TalkUIに設定されているMessageスクリプト・Nameスクリプトを設定
    [SerializeField]
    private TextScript TextSystemScript;

    //TalkUIを設定
    [SerializeField]
    public GameObject obj;

    //表示させるメッセージ
    private string message = "あかさたな\n" +
        "なにぬねの<>" +
        "あいうえお<>" +
        "ああああああああああああああああ<>" +
        "いいいいいいいいいいいいいいいい<>" +
        "ううううううううううううう<>" +
        "えええええええええええええ<>" +
        "おおおおおおおおおおおおおおおお";


    //表示させる名前
    private string charaName = "JK<>" +
        "???<>" +
        "ああああ<>" +
        "うえうえ<>" +
        "はははは<>" +
        "つつつつ<>" +
        "えええええ";

    //表示させるアイコン名　※アイコン名は画像名と同じにすること
    private string charaIcon = "主人公困り<>" +
        "公1<>" +
        "主人公困り<>" + "主人公困り<>" + "主人公困り<>" + "主人公困り<>" + "主人公困り";

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            obj.SetActive(true);
            //表示したいテキスト、キャラ名、キャラアイコンを送る
            TextSystemScript.SetTextPanel(message, charaName, charaIcon, charaIcon);
        }
    }

    /*以上の内容があればメッセージを送ることができる*/
}
