//名前・アイコン表示用

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkName : MonoBehaviour
{
    //TalkUIに設定されているNameスクリプトを設定
    [SerializeField]
    private NameSystem nameScript;


    //表示させる名前
    private string charaName = "JK<>" +
        "???<>" +
        "ああああ<>" +
        "うえうえ<>" +
        "はははは<>" +
        "つつつつ<>" +
        "えええええ";
    //表示させるアイコン名
    private string charaIcon = "主人公困り<>" +
        "公1<>" +
        "主人公困り<>" + "主人公困り<>" + "主人公困り<>" + "主人公困り<>" + "主人公困り" ;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            nameScript.SetNamePanel(charaName, charaIcon);

        }
    }
}
