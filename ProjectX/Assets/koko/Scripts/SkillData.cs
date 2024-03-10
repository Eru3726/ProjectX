using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{
    public enum SKILL_DATA
    {
        zero,
        NM,
        ND,
        LM,
        LB,
        AM,
        AF,
        AC,
        AA,
        SB,
        SD,
        Num
    }

    public enum ACT_DATA
    {
        Zero,   // 0
        NM1,    // 1
        NM2,    // 2
        NM3,    // 3
        ND1,    // 4
        LM1,    // 5
        LB1,    // 6
        AM1,    // 7
        AM2,    // 8
        AM3,    // 9
        AF1,    // 10
        AF2,    // 11
        AC1,    // 12
        AA1,    // 13
        SB1,    // 14
        SD1,    // 15
        Num     // 16
    }

    public enum ATK_DATA
    {
        Zero,   // 0
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
