namespace EruGSS
{
    [System.Serializable]
    public class WebData
    {
        //変数名はGSSのA列で指定した変数名と揃えること
        //必ずstring型

        //第一段階のエリスデータ
        public string ElisData_HitPoint;
        public string ElisData_FallingAttackPower;
        public string ElisData_DefensePower;
        public string ElisData_MoveSpeed;

        //第一段階のエリスの魔法弾データ
        public string ElisData_B_AttackPower;
        public string ElisData_B_MoveSpeed;
        public string ElisData_B_MoveTime;
    }
}