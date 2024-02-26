using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutLine_Item : MonoBehaviour
{
    // 音源
    [SerializeField] Mng_Game Manager;

    // 共に同じ要素数
    // 一段目のパネル
    [SerializeField] GameObject[] Item_F;
    // 二段目のパネル
    [SerializeField] GameObject[] Item_S;
    // アイテムの名前と説明用テキスト
    [SerializeField] Text ItemName;
    [SerializeField] Text ItemExp;
    [SerializeField] ItemDataBase itemdataBase;
    int Xnum = 0;
    int Ynum = 0;
    int XMax;
    int YMax;
    public List<List<GameObject>> ItemObj_List = new List<List<GameObject>>()//リストの宣言
    {
      // Listの中にListを追加して初期化
      new List<GameObject>(),//[0]の中の配列
      new List<GameObject>(),//[1]の中の配列
    };
    void Start()
    {
        //　0～8の長さ9
        XMax = (int)Item_F.Length-1;
        // 0,1の上下二段
        YMax = 1;
        // リスト[0]に一段目を入れる
        for (int i = 0; i < Item_F.Length; i++)
        {
            ItemObj_List[0].Add(Item_F[i]);
        }
        for (int i = 0; i < Item_S.Length; i++)
        {
            ItemObj_List[1].Add(Item_S[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        XYmove();
        itemText();
    }
    void InputKey()
    {
        XKey();
        Ykey();
    }
    void XKey()  // 左右キー
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (Xnum > 0)
            {
                Xnum--;
            }
            else if (Xnum == 0) // 左端に来たら右端に戻す
            {
                Xnum = XMax;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (Xnum<XMax)
            {
                Xnum++;
            }
            else if (Xnum == XMax)// 右端に来たら左端に戻す
            {
                Xnum = 0;
            }
        }
    }
    void Ykey()  // 上下キー
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (Ynum == YMax)
            {
                Ynum--;
            }
            else if (Ynum == 0) // 最上部に来たら最下部にする
            {
                Ynum = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (Ynum == 0)
            {
                Ynum++;
            }
            else if (Ynum == YMax)// 最下部に来たら最上部にする
            {
                Ynum = 0;
            }
        }
    }
    // アウトラインのRectを動かす関数
    void XYmove()
    {
        // アウトラインの位置をリストに登録されている座標位置に合わせる
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = 
            ItemObj_List[Ynum][Xnum].GetComponent<RectTransform>().anchoredPosition+new Vector2(0,-80.0f);
    }
    void itemText()
    {
        for (int i = 0; i < itemdataBase.ItemDATA.Count; i++)
        {
            ItemName.text =
                ItemObj_List[Ynum][Xnum].GetComponent<Image>().sprite == itemdataBase.ItemDATA[i].iIcon ?
               itemdataBase.ItemDATA[i].iname : "アイテム名";
            ItemExp.text=
                ItemObj_List[Ynum][Xnum].GetComponent<Image>().sprite == itemdataBase.ItemDATA[i].iIcon ?
               itemdataBase.ItemDATA[i].infomation : "アイテム説明";
            // 名前が変更出来たらループ処理を脱出
            if (ItemName.text == itemdataBase.ItemDATA[i].iname)
            {
                Debug.Log(itemdataBase.ItemDATA[i].iname);
                return;
            }
        }
        //itemdataBase.ItemDATA[i].iname
    }
}
