using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElisManager : MonoBehaviour
{
    //Neutral
    //Move
    //Shot
    //Avatar
    //FallingAttack

    [SerializeField]
    private LastBossData lastBossData;

    void Start()
    {
        Debug.Log(lastBossData.ElisFastData[0].hp);
        Debug.Log(lastBossData.ElisFastData[0].attackPower);
        Debug.Log(lastBossData.ElisFastData[0].defensePower);
        Debug.Log(lastBossData.ElisFastData[0].speed);

        Debug.Log(lastBossData.ElisSecondData[0].hp);
        Debug.Log(lastBossData.ElisSecondData[0].attackPower);
        Debug.Log(lastBossData.ElisSecondData[0].defensePower);
        Debug.Log(lastBossData.ElisSecondData[0].speed);
    }
}
