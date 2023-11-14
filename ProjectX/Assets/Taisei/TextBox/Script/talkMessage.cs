//呼び出すメッセージを設定する

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkMessage : MonoBehaviour
{
    //TalkUIに設定されているMessageスクリプト・Nameスクリプトを設定
    [SerializeField]
    private TextSystem talkScript;
    

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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //objを有効化
            obj.SetActive(true);
            talkScript.SetMessagePanel(message);
        }
    }
}
