using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutLine_Menu : MonoBehaviour
{
    [SerializeField]RectTransform rtf;
    int[] angle = new int[] { 0,120,240};
    public int num = 0;
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

    void Update()
    {
        // 上入力
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (num == 0)
            {
                num = 2;
            }
            else 
            {
                num--;
            }
        }
        // 下入力
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (num == 2)
            {
                num = 0;
            }
            else
            {
                num++;
            }
        }
        switch(num)
        {
            case 0:
                expText.text = "アイテムの確認/使用";
                ObjAngle(0);
                ObjActive(0);
                break;
            case 1:
                expText.text = "スキルポイント割り振り";
                ObjAngle(1);
                ObjActive(1);
                break;
            case 2:
                expText.text = "ゲーム内設定";
                ObjAngle(2);
                ObjActive(2);
                break;
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
            Fadeitem[num].SetActive(true);
            this.gameObject.GetComponent<OutLine_Menu>().enabled = false;
        }
    }
    void ObjAngle(int i)
    {
        rtf.rotation = Quaternion.Euler(0, 0, angle[i]);
    }
}
