using UnityEngine;
using UnityEngine.Networking;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EruGSS
{
    public class LoadGSS
    {
        private ElisData elisData;
        private ElisSecondData elisSecondData;
        private PathScriptableObject pathScriptableObject;

        public void DataLoad(string pso)
        {
            pathScriptableObject = AssetDatabase.LoadAssetAtPath<PathScriptableObject>(pso);
            elisData = AssetDatabase.LoadAssetAtPath<ElisData>(pathScriptableObject.DataScriptableObject_PATH[0]);
            elisSecondData = AssetDatabase.LoadAssetAtPath<ElisSecondData>(pathScriptableObject.DataScriptableObject_PATH[1]);

            //URLへアクセス
            UnityWebRequest req = UnityWebRequest.Get(pathScriptableObject.GAS_URL);
            req.SendWebRequest();

            while (!req.isDone)
            {
                // リクエストが完了するのを待機
            }

            //変数に反映
            if (IsWebRequestSuccessful(req)) ReflectData(JsonUtility.FromJson<WebData>(req.downloadHandler.text));
            //リクエスト失敗
            else Debug.Log("Error Request Failed");
        }

        //リクエストが成功したかどうか判定する関数
        private bool IsWebRequestSuccessful(UnityWebRequest req)
        {
            //プロトコルエラーとコネクトエラーではない場合はtrueを返す
            return req.result != UnityWebRequest.Result.ProtocolError &&
                   req.result != UnityWebRequest.Result.ConnectionError;
        }

        /// <summary>
        /// 変数の反映
        /// </summary>
        /// <param name="data"></param>
        private void ReflectData(WebData data)
        {
            //変数の型によって変える必要あり
            //generalParameter.intParam = (int)float.Parse(data.key_0);              //int型の場合
            //generalParameter.floatParam = float.Parse(data.key_1);                 //float型の場合
            //generalParameter.stringParam = data.key_2;                             //string型の場合
            //generalParameter.boolParam = data.key_3 == "true" ? true : false;      //bool型の場合

            //第一段階のエリスデータ
            elisData.HitPoint = (int)float.Parse(data.ElisData_HitPoint);
            elisData.FallingAttackPower = (int)float.Parse(data.ElisData_FallingAttackPower);
            elisData.DefensePower = (int)float.Parse(data.ElisData_DefensePower);
            elisData.MoveSpeed = float.Parse(data.ElisData_MoveSpeed);
            elisData.ShotNum = (int)float.Parse(data.ElisData_ShotNum);
            elisData.ShotTime = float.Parse(data.ElisData_ShotTime);
            elisData.WaitTime = float.Parse(data.ElisData_WaitTime);

            //第一段階のエリスの分身データ
            elisData.A_MoveSpeed = float.Parse(data.ElisData_A_MoveSpeed);
            elisData.A_ShotNum = (int)float.Parse(data.ElisData_A_ShotNum);
            elisData.A_ShotTime = float.Parse(data.ElisData_A_ShotTime);
            elisData.A_WaitTime = float.Parse(data.ElisData_A_WaitTime);

            //第一段階のエリスの魔法弾データ
            elisData.B_AttackPower = (int)float.Parse(data.ElisData_B_AttackPower);
            elisData.B_MoveSpeed = float.Parse(data.ElisData_B_MoveSpeed);
            elisData.B_MoveTime = (int)float.Parse(data.ElisData_B_MoveTime);

            //第二段階のエリスデータ
            elisSecondData.ElisSecondData_HitPoint = (int)float.Parse(data.ElisSecondData_HitPoint);
            elisSecondData.ElisSecondData_FallingAttackPower = (int)float.Parse(data.ElisSecondData_FallingAttackPower);
            elisSecondData.ElisSecondData_DefensePower = (int)float.Parse(data.ElisSecondData_DefensePower);
            elisSecondData.ElisSecondData_MoveSpeed = float.Parse(data.ElisSecondData_MoveSpeed);
                               
            //第二段階のエリスの魔法弾データ
            elisSecondData.ElisSecondData_B_AttackPower = (int)float.Parse(data.ElisSecondData_B_AttackPower);
            elisSecondData.ElisSecondData_B_MoveSpeed = float.Parse(data.ElisSecondData_B_MoveSpeed);
            elisSecondData.ElisSecondData_B_MoveTime = (int)float.Parse(data.ElisSecondData_B_MoveTime);

            Debug.Log("GSS反映完了");
        }
    }
}