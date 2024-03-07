using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public partial class Mng_Game : MonoBehaviour
{
    // メニュー開いている間時間を止めるよ
    [SerializeField] GameObject obj;
    [SerializeField] GameObject Text;

    // 曲のセレクト番号
    int BgmNum=0;

    // シーン名でBGMを変更
    // シーン名取得用
    string GetSceneName;
    void Start()
    {
        // 音量の初期化
        aud[0].volume = 0.5f;  // SE
        aud[1].volume = 0.5f;  // BGM
        AudioListener.volume = 0.5f; // MASTER

        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        // シーン名取得
        GetSceneName = SceneManager.GetActiveScene().name;
        aud[1].clip = BGM[BgmNum];
        aud[1].Play();
    }
    // Update is called once per frame
    void Update()
    {
        // ポーズ用時間停止
        Time.timeScale = (obj.activeSelf||Text.activeSelf) ? 0 : 1;
    }

    // シーンチェンジ判定
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        GetSceneName = nextScene.name;
        switch(GetSceneName)
        {
            case "TitleScene": // タイトル
                BgmNum = 0;
                break;
            case "Heaven1": // 天界
                BgmNum = 1;
                break;
            case "Heaven2": // 天界
                BgmNum = 1;
                break;
            case "Hell":    // 冥界
                BgmNum = 2;
                break;
            case "Hut":     // 部屋？
                BgmNum = 3;
                break;
            case "Room2":   // 部屋
                BgmNum = 4;
                break;
            case "Cave":    // 洞窟        ->グレン戦の曲にチェンジ BgmNum=9;
                BgmNum = 5;
                break;
            case "Castle":  // 城
                BgmNum = 6;
                break;
            case "Boss":    // ボス
                BgmNum = 7;
                break;
            case "World":   // 一応設定
                BgmNum = 8;
                break;
            case "Room1":   // 部屋
                BgmNum = 9;
                break;
        }
        aud[1].clip = BGM[BgmNum];
        aud[1].Play();
    }
}

// 音関連のマネージャーはMng_Sound.csにて分割