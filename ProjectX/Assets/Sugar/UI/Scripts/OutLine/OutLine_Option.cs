using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutLine_Option : MonoBehaviour
{
    RectTransform rtf;
    // 設定項目
    [SerializeField] GameObject[] item;
    [SerializeField] Text[] text;
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    void Update()
    {
        // 上入力
        if (Input.GetKeyDown(KeyCode.W)&&
            rtf.anchoredPosition != new Vector2(150,-50)) 
        { rtf.anchoredPosition += new Vector2(0, 100); }
        // 下入力
        else if (Input.GetKeyDown(KeyCode.S)&& 
            rtf.anchoredPosition != new Vector2(150,-250)) 
        { rtf.anchoredPosition += new Vector2(0, -100); }

        // 操作設定項目
        if      (rtf.anchoredPosition == new Vector2(150, -50))
        { ObjActive(0); }
        // ディスプレイ項目
        else if (rtf.anchoredPosition == new Vector2(150, -150))
        { ObjActive(1); }
        // オーディオ項目
        else if (rtf.anchoredPosition == new Vector2(150, -250))
        { ObjActive(2); }
    }

    // 全て非表示にしてから特定のオブジェクトを表示
    void ObjActive(int num)
    {
       for(int i=0;i<item.Length;i++)
        {
            item[i].SetActive(false);
            text[i].fontStyle = FontStyle.Normal;
        }
        item[num].SetActive(true);
        text[num].fontStyle = FontStyle.Bold;
    }
}
