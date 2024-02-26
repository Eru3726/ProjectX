using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutLine_Menu : MonoBehaviour
{
    [SerializeField] Mng_Game Manager;

    [SerializeField]RectTransform rtf;
    int[] angleL = new int[] { 0,120,240};
    int[] angleR = new int[] { 0,-240,-120};
    public int targetRot = 0;
    int countZ = 0;
    int InputLR = 0;
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
        // 左入力
        if (Input.GetKeyDown(KeyCode.A))
        {
            InputLR = -1;
            targetRot -= 120;
            if(targetRot==-360)
            {
                targetRot = 0;
            }
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (num == 0)
            {
                num = 2;
            }
            else 
            {
                num--;
            }
        }
        // 右入力
        else if (Input.GetKeyDown(KeyCode.D))
        {
            InputLR = 1;
            targetRot += 120;
            if (targetRot == 360)
            {
                targetRot = 0;
            }
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
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
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.enter);
            Fadeitem[num].SetActive(true);
            this.gameObject.GetComponent<OutLine_Menu>().enabled = false;
        }
    }
    void ObjAngle(int i)
    {
        if (countZ == 360||countZ==-360)
        {
            countZ = 0;
        }
        if (rtf.localRotation == Quaternion.Euler(0, 0, angleL[0]))
        { 
            countZ = 0;
        }
        else if (rtf.localRotation == Quaternion.Euler(0, 0, angleR[0]))
        {
            countZ = 0;
        }
        else if(rtf.localRotation == Quaternion.Euler(0, 0, angleL[1]))
        {
            countZ = 120;
        }
        else if (rtf.localRotation == Quaternion.Euler(0, 0, angleR[1]))
        {
            countZ = -240;
        }
        else if (rtf.localRotation == Quaternion.Euler(0, 0, angleL[2]))
        {
            countZ = 240;
        }
        else if (rtf.localRotation == Quaternion.Euler(0, 0, angleR[2]))
        {
            countZ = -120;
        }
        if (countZ!= targetRot&& InputLR == 1)
        {
            countZ += 10;
            rtf.localRotation = Quaternion.Euler(0, 0, countZ);
        }
        else if (countZ!=targetRot&& InputLR==-1)
        {
            countZ -= 10;
            rtf.localRotation = Quaternion.Euler(0, 0, countZ);
        }
    }
}
