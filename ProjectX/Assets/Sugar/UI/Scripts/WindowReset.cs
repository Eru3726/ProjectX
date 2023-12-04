using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowReset : MonoBehaviour
{
    [SerializeField] GameObject[] item;
    
    // 初期座標
    [Header("0:X座標 1:Y座標 2:Z座標")]
    [SerializeField] float[] strPos;
    void OnEnable() 
    {
        // このオブジェクトの座標を初期位置にする
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(strPos[0], strPos[1], strPos[2]);

        // メニュー以外非表示にする
        for(int i=0;i<item.Length;i++)
        {
            item[i].SetActive(false);
        }
        item[0].SetActive(true);
    }
}
