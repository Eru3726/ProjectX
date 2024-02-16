using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SEをデータベースにまとめておく
[CreateAssetMenu(fileName = "SEDataBase", menuName = "CreateSEDataBase")]
public class SEDataBase : ScriptableObject
{
    [Header("0:PL 1:EM 2:UI")]
    public List<SEData> SEDATA = new List<SEData>();
}

