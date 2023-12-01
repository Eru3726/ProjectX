using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData
{
    public enum SKILL_DATA
    {
        N,      // 0
        NM1,    // 1
        NM2,    // 2
        NM3,    // 3
        LM1,    // 4
        LB1,    // 5
        AM1,    // 6
        AM2,    // 7
        AM3,    // 8
        AF1,    // 9
        AC1,    // 10
        AA1,    // 11
        ND1,    // 12
        SB1,    // 13
        SD1,    // 14
        Num     // 15
    }

    public enum ATK_DATA
    {
        N,      // 0
        NM1,    // 1
        NM2,    // 2
        NM3,    // 3
        LM1,    // 4
        LM2,    // 5
        LM3,    // 6
        LM4,    // 7
        LM5,    // 8
        LM6,    // 9
        LB1,    // 10
        AM1,    // 11
        AM2,    // 12
        AM3,    // 13
        AF1,    // 14
        AF2,    // 15
        AF3,    // 16
        AF4,    // 17
        AF5,    // 18
        AF6,    // 19
        AF7,    // 20
        AF8,    // 21
        AF9,    // 22
        AF10,   // 23
        AF11,   // 24
        AF12,   // 25
        AC1,    // 26
        AA1,    // 27
        Num     // 28
    }

    public enum LAYER_DATA
    {
        Neutral,
        Player,
        Enemy
    }
}
