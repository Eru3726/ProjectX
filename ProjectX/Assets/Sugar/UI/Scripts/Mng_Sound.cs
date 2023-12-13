using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Mng_Game : MonoBehaviour
{
    // SEとBGM用
    [Header("0:SE 1:BGM")]
    [SerializeField] AudioSource[] aud;

    // マスターボリューム
    public float Para_Master
    {
        set
        {
            AudioListener.volume = value;
        }
    }
    // SE
    // サウンド調整
    public float Para_Se
    {
        set
        {
            aud[0].volume = value;
        }
    }

    // BGM
    // サウンド調整
    public float Para_Bgm
    {
        set
        {
            aud[1].volume = value;
        }
    }
    // 仮で必要になりそうなものを準備。
    // 追加で必要ならここに用意　
    public enum ClipSe 
    {
        Move,
        Jump,
        Atk1,
        Atk2,
        Hit1,
        Hit2,
        Skill1,
        Skill2,
        Death,
    }

    public enum UISe
    {
        textmsg,
        enter,
        wasd,
        esc,
    }

    // インスペクターから分かりやすいように
    [Header("主にキャラ用PL EM含め")]
    [Header("0:Move 1:Jump 2:Atk1\n" +
        "3:At2k 4:Hit1 5:Hit2 \n " +
        "6:Skill1 7:skill2 8:Death ")]
    [SerializeField]  // 予めSEの音声データを入れておく
    AudioClip[] SETbl1;


    [Header("主にUI用 選択音とかテキスト音")]
    [Header("0:TextMessage 1:Enter 2:WASD 3:ESC")]
    [SerializeField]  // 予めSEの音声データを入れておく
    AudioClip[] SETbl2;

    // 必要な時にそれぞれから呼び出し

    /// <summary>
    /// ClipSe.xxxで必要なSEを再生
    /// </summary>
    /// <param name="clipse"></param>
    void OneShotSE_Chara(ClipSe clipse)
    {
        aud[0].PlayOneShot(SETbl1[(int)clipse]);
    }
    void OneShotSE_Chara(UISe se)
    {
        aud[0].PlayOneShot(SETbl2[(int)se]);
    }
}

