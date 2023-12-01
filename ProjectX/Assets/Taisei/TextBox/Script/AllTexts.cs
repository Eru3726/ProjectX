using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTexts : MonoBehaviour
{
    [SerializeField]
    private TextScript TextSystemScript;

    //表示させるメッセージ
    private string message;


    //表示させる名前
    private string charaName;

    //表示させるアイコン名　※アイコン名は画像名と同じにすること
    private string charaIconLeft;
    private string charaIconRight;

    //使い方
    //・ここにテキスト・名前・アイコン名などを書き込んでいく
    //・台詞・名前・アイコン名をここで一括管理する
    //・「<>」があるとこまでがひとつの台詞となる
    //・一番最後のところに「<>」があると動作が上手くいかなくなるため、
    //　つけないように注意してください
    //・assetフォルダーにResourcesフォルダーを作り、そこにアイコン用の画像を入れる
    //・ここに書くアイコン名はアイコン用画像の名前と同じにする
    //
    //表示させるとき
    //スクリプトに以下の文を追加
    //[SerializeField] private AllTexts alltextsscript;
    //[SerializeField] private GameObject talkUI;
    //int textNo;
    //上二つはどちらともゲームオブジェクトの「TalkUI」を設定する
    //
    //以下の文をupdateなどに追加することで台詞などを表示する
    //textNo = ○〇;←呼び出したいセリフの番号を設定
    //talkUI.SetActive(true);
    //alltextsscript.SetAllTexts(textNo);
    //
    //※使用例→「testSet」スクリプト


    public void SetAllTexts(int textNo)
    {
        Debug.Log("仮");
        switch (textNo)
        {
            case 0:
                message = "こんにちは<>" +
                    "これはテストです<>" +
                    "改行すると\nこうなります。";
                charaName = "あ<>" +
                    "(´・ω・｀)<>" +
                    "やぁ";
                charaIconLeft = "主人公困り<>" +
                "公1<>" +
                "主人公困り<>";
                charaIconRight = "公1<>" +
                    "主人公困り<>" +
                    "公1";
                Debug.Log("おｋ");
                break;

            case 1:
                message = "こんばんは<>" +
                    "第2のテストです";
                charaName = "？？？<>" +
                    "博士";
                charaIconLeft = "公2<>" +
                    "公3";
                break;
        }

        TextSystemScript.SetTextPanel(message, charaName, charaIconLeft, charaIconRight);

    }
}
