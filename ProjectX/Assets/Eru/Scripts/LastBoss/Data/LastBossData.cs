using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class LastBossData : ScriptableObject
{
    public List<ElisData> ElisFastData; // Replace 'EntityType' to an actual type that is serializable.
    public List<ElisSecondData> ElisSecondData; // Replace 'EntityType' to an actual type that is serializable.
}
