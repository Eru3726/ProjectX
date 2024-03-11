using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class ItemData : ScriptableObject
{
    public enum Type // 実装するSEの種類
    {
        Key,
        SingleUse,
    }
    public bool getFlag = false;
    public Sprite iIcon;       // アイテムアイコン
    public string iname;         // アイテムの名前
    public string infomation;   // 情報(アイテム説明)
    public Type type;           // 種類

    public ItemData(ItemData idata)
    {
        this.type = idata.type;
        this.iIcon = idata.iIcon;
        this.infomation = idata.infomation;
        this.iname = idata.iname;
        this.getFlag = idata.getFlag;
    }
}