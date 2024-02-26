using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSet : MonoBehaviour
{
    [SerializeField] ItemDataBase itemdataBase;
    // 共に同じ要素数
    // 一段目のパネル
    [SerializeField] Image[] Item_F;
    // 二段目のパネル
    [SerializeField] Image[] Item_S;

    
    public List<List<Image>> Image_List = new List<List<Image>>()//リストの宣言
    {
      // Listの中にListを追加して初期化
      new List<Image>(),//[0]の中の配列
      new List<Image>(),//[1]の中の配列
    };

    // Start is called before the first frame update
    void Start()
    {
        // リスト[0]に一段目を入れる
        for (int i = 0; i < Item_F.Length; i++)
        {
            Image_List[0].Add(Item_F[i]);
        }
        for (int i = 0; i < Item_S.Length; i++)
        {
            Image_List[1].Add(Item_S[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Image_List[0].Count);
        for(int i=0; i< itemdataBase.ItemDATA.Count;i++)
        {
            if(itemdataBase.ItemDATA[i].getFlag)
            {
                for (int j = 0; j < Image_List[0].Count; j++)
                {
                    if (Image_List[0][j].sprite == null)
                    {
                        Image_List[0][j].sprite = itemdataBase.ItemDATA[i].iIcon;
                        itemdataBase.ItemDATA[i].getFlag = false;
                        break;
                    }
                    else if (Image_List[0][8].sprite != null)
                    {
                        if (Image_List[1][j].sprite == null)
                        {
                            Image_List[1][j].sprite = itemdataBase.ItemDATA[i].iIcon;
                            itemdataBase.ItemDATA[i].getFlag = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}
