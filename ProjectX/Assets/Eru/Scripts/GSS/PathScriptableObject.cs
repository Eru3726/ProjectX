using UnityEngine;

namespace EruGSS
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PathScriptableObject")]
    public class PathScriptableObject : ScriptableObject
    {
        [Header("GSSのURL")]
        public string Sheet_URL;

        [Header("GASのURL")]
        public string GAS_URL;

        [Header("GeneralParameter.assetのパス")]
        public string GeneralParameter_PATH;

        [Header("変数を追加するためのスクリプトのパス")]
        public string[] Scripts_PATH = new string[3];
    }
}