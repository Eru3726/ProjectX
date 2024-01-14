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

        //第二段階のエリスデータ
        public string ElisSecondData_HitPoint;
        public string ElisSecondData_FallingAttackPower;
        public string ElisSecondData_DefensePower;
        public string ElisSecondData_MoveSpeed;
                          
        //第二段階のエリスの魔法弾データ
        public string ElisSecondData_B_AttackPower;
        public string ElisSecondData_B_MoveSpeed;
        public string ElisSecondData_B_MoveTime;
    }
}