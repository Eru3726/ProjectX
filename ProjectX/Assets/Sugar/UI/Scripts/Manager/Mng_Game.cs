using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public partial class Mng_Game : MonoBehaviour
{
    // メニュー開いている間時間を止めるよ
    [SerializeField] GameObject obj;
    void Start()
    {
        // 音量の初期化
        aud[0].volume = 0.5f;  // SE
        aud[1].volume = 0.5f;  // BGM
        AudioListener.volume = 0.5f; // MASTER
    }
    // Update is called once per frame
    void Update()
    {
        // ポーズ用時間停止
        Time.timeScale = (obj.activeSelf) ? 0 : 1;

        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}

// 音関連のマネージャーはMng_Sound.csにて分割