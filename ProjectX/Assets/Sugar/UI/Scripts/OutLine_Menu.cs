using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutLine_Menu : MonoBehaviour
{
    RectTransform rtf;
    // 設定項目
    [Header("0:アイテムウィンドウ\n" +
        " 1:スキルウィンドウ\n" +
        " 2:システムウィンドウ")]
    [SerializeField] GameObject[] item;
    [SerializeField] GameObject[] Fadeitem;
    // フォント変えたいから
    [SerializeField] Text[] text;
    // 今選んでる項目の説明を表示するためのText
    [SerializeField] Text expText;
    // 他のものを起動した時にこれらを閉じるように
    [SerializeField] GameObject Menu;
    bool INPUT = false;
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        INPUT = false;
    }
    void Update()
    {
        if (INPUT) { return; }
        // 上入力
        if (Input.GetKeyDown(KeyCode.W) &&
            rtf.anchoredPosition != new Vector2(525, -340))
        { rtf.anchoredPosition += new Vector2(0, 200); }
        // 下入力
        else if (Input.GetKeyDown(KeyCode.S) &&
            rtf.anchoredPosition != new Vector2(525, -740))
        { rtf.anchoredPosition += new Vector2(0, -200); }

        // 操作設定項目
        if (rtf.anchoredPosition == new Vector2(525, -340))
        {
            rtf.rotation= Quaternion.Euler(0, 0, 20);
            expText.text = "アイテムの確認/使用";
            ObjActive(0);
        }
        // ディスプレイ項目
        else if (rtf.anchoredPosition == new Vector2(525, -540))
        {
            rtf.rotation = Quaternion.Euler(0, 0, 0);
            expText.text = "スキルポイント割り振り";
            ObjActive(1);
        }
        // オーディオ項目
        else if (rtf.anchoredPosition == new Vector2(525, -740))
        {
            rtf.rotation = Quaternion.Euler(0, 0, -20);
            expText.text = "ゲーム内設定";
            ObjActive(2);
        }
    }

    // 全て非表示にしてから特定のオブジェクトを表示
    void ObjActive(int num)
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i].SetActive(false);
            text[i].fontStyle = FontStyle.Normal;
        }
        text[num].fontStyle = FontStyle.Bold;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            INPUT = true;
            Fadeitem[num].SetActive(true);
        }
    }
}
