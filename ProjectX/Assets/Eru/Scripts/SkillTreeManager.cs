using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
        escapefromFear = 1 << 16,           //恐怖からの逃亡
        panic = 1 << 17,                    //パニック
        tension = 1 << 18,                  //緊張
        anxiety = 1 << 19,                  //不安
        suffering = 1 << 20,                //苦悩
        grief = 1 << 21,                    //悲哀
        despairforLife = 1 << 22,           //命への失望
        recklessness = 1 << 23,             //自暴自棄
        resignation = 1 << 24,              //諦め
        sorrow = 1 << 25,                   //悲哀
        empty1 = 1 << 26,                   //
        Hopelessness = 1 << 27,             //絶望
        empty2 = 1 << 28,                   //
        Powerlessness = 1 << 29,            //無力
        empty3 = 1 << 30,                   //

        //恋
        love = 1 << 31,                     //愛
    }

    [HideInInspector]
    public SkillTree skillData;



    void Awake()
    {
        Load();
    }

    void Update()
    {
        
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
        }
    }

    // セーブデータの作成
    private SkillTreeSaveData CreateSaveData()
    {
        //セーブデータのインスタンス化
        SkillTreeSaveData saveData = new SkillTreeSaveData
        {
            skillSaveData = skillData
        };

        return saveData;
    }

    //データの読み込み（反映）
    private void ReadData(SkillTreeSaveData saveData)
    {
        skillData = saveData.skillSaveData;
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


[System.Serializable]
public class SkillTreeSaveData
{
    public SkillTreeManager.SkillTree skillSaveData;
}
