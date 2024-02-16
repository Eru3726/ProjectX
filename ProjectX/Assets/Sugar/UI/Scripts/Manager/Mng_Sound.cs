using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    // キャラクター関連の音
    public enum ClipSe 
    {
        Move1,
        Move2,
        Jump1,
        Jump2,
        Atk1,
        Atk2,
        Hit1,
        Hit2,
        Skill1,
        Skill2,
        Death,
        num,
    }
    // UI関連の音はこっち
    public enum UISe
    {
        textmsg, // テキスト送り音
        enter,   // 決定音
        wasd,    // UIの移動音
        esc,     // UIウィンドウ
        tab,     // 戻る音（キャンセル音）
        num,
    }
    const int Charanum = (int)ClipSe.num;
    const int UInum = (int)UISe.num;

    [SerializeField] SEDataBase dataBase;

    /// <summary>
    /// Mng_Sound.ClipSe.xxxで必要なSEを再生
    /// </summary>
    /// <param name="clipse"></param>
    public void OneShotSE_C(SEData.Type type,ClipSe se)
    {
        if(type==SEData.Type.PlayerSE)
        {
            aud[0].PlayOneShot(dataBase.SEDATA[(int)SEData.Type.PlayerSE].SE[(int)se]);
        }
        else if(type==SEData.Type.EnemySE)
        {
            aud[0].PlayOneShot(dataBase.SEDATA[(int)SEData.Type.EnemySE].SE[(int)se]);
        }
    }
    public void OneShotSE_U(SEData.Type type,UISe se)
    {
        aud[0].PlayOneShot(dataBase.SEDATA[(int)SEData.Type.UISE].SE[(int)se]);
    }
}
// OneShotSE_C(SEData.Type.EnemySE, Mng_Game.ClipSe.Atk1);
