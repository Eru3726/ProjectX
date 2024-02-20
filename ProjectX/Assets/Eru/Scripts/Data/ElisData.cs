using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ElisData")]
public class ElisData : ScriptableObject
{
    //実際にゲーム内で使う変数はこれ
    //変数名と型は自由

    //第一段階のエリスデータ
    public int HitPoint;
    public int FallingAttackPower;
    public int DefensePower;
    public float MoveSpeed;
    public int ShotNum;
    public float ShotTime;
    public float WaitTime;

    //第一段階のエリスの分身データ
    public float A_MoveSpeed;
    public int A_ShotNum;
    public float A_ShotTime;
    public float A_WaitTime;

    //第一段階のエリスの魔法弾データ
    public int B_AttackPower;
    public float B_MoveSpeed;
    public int B_MoveTime;
}

