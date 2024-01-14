using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ElisSecondData")]
public class ElisSecondData : ScriptableObject
{
    //実際にゲーム内で使う変数はこれ
    //変数名と型は自由

    //第二段階のエリスデータ
    public int ElisSecondData_HitPoint;
    public int ElisSecondData_FallingAttackPower;
    public int ElisSecondData_DefensePower;
    public float ElisSecondData_MoveSpeed;

    //第二段階のエリスの魔法弾データ
    public int ElisSecondData_B_AttackPower;
    public float ElisSecondData_B_MoveSpeed;
    public int ElisSecondData_B_MoveTime;
}
