using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイテム情報
[CreateAssetMenu(fileName = "ItemDataBase", menuName = "CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> ItemDATA = new List<ItemData>();
}
