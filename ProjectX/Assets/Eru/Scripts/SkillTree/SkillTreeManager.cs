using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Eru
{
    public class SkillTreeManager : MonoBehaviour
    {
        [Flags]
        public enum SkillTree
        {
            //怒り
            disgust = 1 << 0,                   //嫌悪
            irritation = 1 << 1,                //いらだち
            jealousy = 1 << 2,                  //嫉妬心
            anger = 1 << 3,                     //憤怒
            resentment = 1 << 4,                //恨み
            rage = 1 << 5,                      //激昂
            chainofHatred = 1 << 6,             //憎しみの連鎖
            burningAnger = 1 << 7,              //燃え上がる怒り
            aversion = 1 << 8,                  //反感
            frustration = 1 << 9,               //欲求不満
            fightingSpirit = 1 << 10,           //闘争心
            angryPrincessTantrum = 1 << 11,     //怒れる姫の癇癪
            swirlingEmotions = 1 << 12,         //渦巻いた感情
            awakening = 1 << 13,                //覚醒
            birthoftheCrimsonQueen = 1 << 14,   //紅ノ女王誕生

            //悲しみ
            tragedy = 1 << 15,                  //悲惨
            escapeFromFear = 1 << 16,           //恐怖からの逃亡
            panic = 1 << 17,                    //パニック
            tension = 1 << 18,                  //緊張
            anxiety = 1 << 19,                  //不安
            suffering = 1 << 20,                //苦悩
            grief = 1 << 21,                    //悲哀
            despairforLife = 1 << 22,           //命への失望
            recklessness = 1 << 23,             //自暴自棄
            resignation = 1 << 24,              //諦め
            empty1 = 1 << 25,                   //
            hopelessness = 1 << 26,             //絶望
            empty2 = 1 << 27,                   //
            powerlessness = 1 << 28,            //無力
            empty3 = 1 << 29,                   //

            //恋
            love = 1 << 30,                     //愛
        }

        [HideInInspector]
        public static SkillTree skillData;

        [Header("所持スキルポイント")]
        public static int skillPoint;

        [SerializeField, Header("スキル説明パネル")]
        private GameObject skillExplanationPanle;

        [SerializeField, Header("SPが足りる時用パネル")]
        private GameObject skillReleasePanle;

        [SerializeField, Header("SPが足りない時用パネル")]
        private GameObject noSPPanle;

        [SerializeField, Header("スキル名テキスト")]
        private Text skillNameText;

        [SerializeField, Header("必要SPテキスト")]
        private Text requiredSPText;

        [SerializeField, Header("説明テキスト")]
        private Text explanationText;

        [SerializeField, Header("NoSpパネル表示時間")]
        private float noSpTime = 1.5f;

        [SerializeField, Header("恋パネル")]
        private GameObject lovePanle;

#if UNITY_EDITOR
        [SerializeField, Header("デバッグ用恋フラグ")]
        private bool debugLoveFlg = false;
#endif

        public static bool loveFlg = false;

        private bool openPanel = false;

        private bool releaseFlg = false;

        private float time = 0;

        private SkillPanel skillP = null;

        [HideInInspector]
        public bool angerFlg = false;

        [HideInInspector]
        public bool sorrowFlg = false;

        void Awake()
        {
            Load();
            skillExplanationPanle.SetActive(false);
            skillReleasePanle.SetActive(false);
            noSPPanle.SetActive(false);
        }

        private void Update()
        {
            // マウスカーソルの位置をスクリーン座標からワールド座標に変換
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // マウスカーソルの位置にRayを飛ばし、ヒットしたコライダーを取得
            Collider2D hitCollider = Physics2D.OverlapPoint(cursorPosition);

            // ヒットしたオブジェクトがUI要素であれば
            if (hitCollider != null && hitCollider.TryGetComponent<SkillPanel>(out SkillPanel skillPanel) && !releaseFlg)
            {
                //判定処理
                openPanel = true;
                if ((int)skillPanel.skillTree <= 20000) skillExplanationPanle.transform.position = new Vector3(Mathf.Abs(skillExplanationPanle.transform.position.x), skillExplanationPanle.transform.position.y, skillExplanationPanle.transform.position.z);
                else skillExplanationPanle.transform.position = new Vector3(-Mathf.Abs(skillExplanationPanle.transform.position.x), skillExplanationPanle.transform.position.y, skillExplanationPanle.transform.position.z);
                skillNameText.text = skillPanel.skillName;
                if (skillPanel.releaseConditions != 0 && (skillPanel.releaseConditions & skillData) == 0
                    || angerFlg && skillPanel.skillTree == SkillTree.empty2
                    || angerFlg && skillPanel.skillTree == SkillTree.powerlessness
                    || sorrowFlg && skillPanel.skillTree == SkillTree.angryPrincessTantrum
                    || sorrowFlg && skillPanel.skillTree == SkillTree.swirlingEmotions) requiredSPText.text = "解放不可";
                else if ((skillPanel.skillTree & skillData) != skillPanel.skillTree) requiredSPText.text = "必要SP:" + skillPanel.requiredSP.ToString();
                else requiredSPText.text = "解放済み";
                explanationText.text = skillPanel.explanation;
                skillP = skillPanel;
            }
            else openPanel = false;

            if (Input.GetMouseButtonDown(0) && skillP != null && openPanel)
            {
                if ((skillP.skillTree & skillData) == skillP.skillTree
                    || skillP.releaseConditions != 0
                    && (skillP.releaseConditions & skillData) == 0
                    || angerFlg && skillP.skillTree == SkillTree.empty2
                    || angerFlg && skillP.skillTree == SkillTree.powerlessness
                    || sorrowFlg && skillP.skillTree == SkillTree.angryPrincessTantrum
                    || sorrowFlg && skillP.skillTree == SkillTree.swirlingEmotions) return;
                if (skillPoint >= skillP.requiredSP) skillReleasePanle.SetActive(true);
                else
                {
                    time = noSpTime;
                    noSPPanle.SetActive(true);
                }
                releaseFlg = true;
            }

            if (!releaseFlg) skillExplanationPanle.SetActive(openPanel);

            if (time <= 0 && releaseFlg && !skillReleasePanle.activeSelf)
            {
                noSPPanle.SetActive(false);
                releaseFlg = false;
            }
            else if (time > 0) time -= Time.deltaTime;

            lovePanle.SetActive(loveFlg);

#if UNITY_EDITOR
            loveFlg = debugLoveFlg;
#endif
        }

        public void YesButton()
        {
            skillData |= skillP.skillTree;
            skillPoint -= skillP.requiredSP;
            if (skillP.skillTree == SkillTree.angryPrincessTantrum || skillP.skillTree == SkillTree.swirlingEmotions) angerFlg = true;
            else if (skillP.skillTree == SkillTree.empty2 || skillP.skillTree == SkillTree.powerlessness) sorrowFlg = true;
            skillP.releaseFlg = true;
            if ((int)skillP.skillTree <= 20000) skillP.image.color = new Color(255 / 255f, 100 / 255f, 100 / 255f, 255 / 255f);
            else skillP.image.color = new Color(100 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);
            skillReleasePanle.SetActive(false);
            releaseFlg = false;
        }

        public void NoButton()
        {
            skillReleasePanle.SetActive(false);
            releaseFlg = false;
        }

        private void OnDestroy()
        {
            Save();
        }

        // 上書き情報の保存
        public void Save()
        {
#if UNITY_EDITOR
            //UnityEditor上なら
            //Assetファイルの中のSaveファイルのパスを入れる
            string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

            //セーブファイルのパスを設定
            string SaveFilePath = path + "/SkillTreeData.bytes";

            // セーブデータの作成
            SkillTreeSaveData saveData = CreateSaveData();

            // セーブデータをJSON形式の文字列に変換
            string jsonString = JsonUtility.ToJson(saveData);

            // 文字列をbyte配列に変換
            byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

            // AES暗号化
            byte[] arrEncrypted = AesEncrypt(bytes);

            // 指定したパスにファイルを作成
            FileStream file = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);

            //ファイルに保存する
            try
            {
                // ファイルに保存
                file.Write(arrEncrypted, 0, arrEncrypted.Length);
            }
            finally
            {
                // ファイルを閉じる
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        public void Load()
        {
#if UNITY_EDITOR
            //UnityEditor上なら
            //Assetファイルの中のSaveファイルのパスを入れる
            string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

            //セーブファイルのパスを設定
            string SaveFilePath = path + "/SkillTreeData.bytes";

            //セーブファイルがあるか
            if (File.Exists(SaveFilePath))
            {
                //ファイルモードをオープンにする
                FileStream file = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
                try
                {
                    // ファイル読み込み
                    byte[] arrRead = File.ReadAllBytes(SaveFilePath);

                    // 復号化
                    byte[] arrDecrypt = AesDecrypt(arrRead);

                    // byte配列を文字列に変換
                    string decryptStr = Encoding.UTF8.GetString(arrDecrypt);

                    // JSON形式の文字列をセーブデータのクラスに変換
                    SkillTreeSaveData saveData = JsonUtility.FromJson<SkillTreeSaveData>(decryptStr);

                    //データの反映
                    ReadData(saveData);

                }
                finally
                {
                    // ファイルを閉じる
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
            else
            {
                //初期化
                skillData = 0;
                skillPoint = 0;
                angerFlg = false;
                sorrowFlg = false;
                loveFlg = false;
            }
        }

        // セーブデータの作成
        private SkillTreeSaveData CreateSaveData()
        {
            //セーブデータのインスタンス化
            SkillTreeSaveData saveData = new SkillTreeSaveData
            {
                skillSaveData = skillData,
                skillPointData = skillPoint,
                angerFlg = angerFlg,
                sorrowFlg = sorrowFlg,
                loveFlg = loveFlg
            };

            return saveData;
        }

        //データの読み込み（反映）
        private void ReadData(SkillTreeSaveData saveData)
        {
            skillData = saveData.skillSaveData;
            skillPoint = saveData.skillPointData;
            angerFlg = saveData.angerFlg;
            sorrowFlg = saveData.sorrowFlg;
            loveFlg = saveData.loveFlg;
        }

        /// <summary>
        ///  AesManagedマネージャーを取得
        /// </summary>
        /// <returns></returns>
        private AesManaged GetAesManager()
        {
            //任意の半角英数16文字
            string aesIv = "1046876440618723";
            string aesKey = "1678140583427606";

            AesManaged aes = new AesManaged
            {
                KeySize = 128,
                BlockSize = 128,
                Mode = CipherMode.CBC,
                IV = Encoding.UTF8.GetBytes(aesIv),
                Key = Encoding.UTF8.GetBytes(aesKey),
                Padding = PaddingMode.PKCS7
            };
            return aes;
        }

        /// <summary>
        /// AES暗号化
        /// </summary>
        /// <param name="byteText"></param>
        /// <returns></returns>
        public byte[] AesEncrypt(byte[] byteText)
        {
            // AESマネージャーの取得
            AesManaged aes = GetAesManager();
            // 暗号化
            byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

            return encryptText;
        }

        /// <summary>
        /// AES復号化
        /// </summary>
        /// <param name="byteText"></param>
        /// <returns></returns>
        public byte[] AesDecrypt(byte[] byteText)
        {
            // AESマネージャー取得
            var aes = GetAesManager();
            // 復号化
            byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

            return decryptText;
        }

        //セーブデータ削除
        public void Init()
        {
#if UNITY_EDITOR
            //UnityEditor上なら
            //Assetファイルの中のSaveファイルのパスを入れる
            string path = Application.dataPath + "/Save";

#else
        //そうでなければ
        //.exeがあるところにSaveファイルを作成しそこのパスを入れる
        Directory.CreateDirectory("Save");
        string path = Directory.GetCurrentDirectory() + "/Save";

#endif

            //ファイル削除
            File.Delete(path + "/SkillTreeData.bytes");

            //リロード
            Load();

            Debug.Log("データの初期化が終わりました");
        }
    }


    [Serializable]
    public class SkillTreeSaveData
    {
        public SkillTreeManager.SkillTree skillSaveData;
        public int skillPointData;
        public bool angerFlg = false;
        public bool sorrowFlg = false;
        public bool loveFlg = false;
    }
}