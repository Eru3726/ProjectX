using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GeneralParameter")]
public class GeneralParameter : ScriptableObject
{
    //実際にゲーム内で使う変数はこれ
    //変数名と型は自由

    //第一段階のエリスデータ
    public int ElisData_HitPoint;
    public int ElisData_FallingAttackPower;
    public int ElisData_DefensePower;
    public float ElisData_MoveSpeed;

    //第一段階のエリスの魔法弾データ
    public int ElisData_B_AttackPower;
    public float ElisData_B_MoveSpeed;
    public int ElisData_B_MoveTime;
}

