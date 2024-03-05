using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "SE", menuName = "CreateSE")]
public class SEData : ScriptableObject
{
    public enum Type // 実装するSEの種類
    {
        PlayerSE,
        EnemySE,
        UISE,
    }

    public Type type; // 種類
    public string infomation; // 情報

    // 別スクリプト参照(Mng_Game.cs)ClipSeのenumの数を入れる
    const int Max = (int)Mng_Game.ClipSe.num;

    [Header("PL EMに使うサンプルリスト\n" +
        "0,1:Move 2,3:Jump 4,5:Atk\n" +
        "6,7:Hit 8,9:Skill 10:Death")]

    [Header("UIに使うサンプルリスト\n" +
        "0:TextMessage 1:Enter\n" +
        "2:WASD 3:ESC 4:Tab" +
        "5:その他Effect1 6:その他Effect2 ")]

    // 音声データ
    public AudioClip[] SE = new AudioClip[Max];     
    public SEData(SEData sedata)
    {
        this.type = sedata.type;
        this.infomation = sedata.infomation;
        this.SE = sedata.SE;
    }
}

