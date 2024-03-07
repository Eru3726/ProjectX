using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTexts : MonoBehaviour
{
    [SerializeField]
    private TextScript TextSystemScript;

    //表示させるメッセージ
    private string message;


    //表示させる名前
    private string charaName;

    //表示させるアイコン名　※アイコン名は画像名と同じにすること
    //private string charaIconLeft;
    //private string charaIconRight;
    private string charaIcon;

    //どっちの立ち絵変更するか
    //true=右 false=左
    private string LorR;

    //アニメーション番号
    private string anims;

    //
    private string csvFiles;

    //使い方
    //・ここにテキスト・名前・アイコン名などを書き込んでいく
    //・台詞・名前・アイコン名をここで一括管理する
    //・「<>」があるとこまでがひとつの台詞となる
    //・一番最後のところに「<>」があると動作が上手くいかなくなるため、
    //　つけないように注意してください
    //・assetフォルダーにResourcesフォルダーを作り、そこにアイコン用の画像を入れる
    //・ここに書くアイコン名はアイコン用画像の名前と同じにする
    //
    //表示させるとき
    //スクリプトに以下の文を追加
    //[SerializeField] private AllTexts alltextsscript;
    //[SerializeField] private GameObject talkUI;
    //int textNo;
    //上二つはどちらともゲームオブジェクトの「TalkUI」を設定する
    //
    //以下の文をupdateなどに追加することで台詞などを表示する
    //textNo = ○〇;←呼び出したいセリフの番号を設定
    //talkUI.SetActive(true);
    //alltextsscript.SetAllTexts(textNo);
    //
    //※使用例→「testSet」スクリプト



    public void SetAllTexts(int textNo)
    {
        switch (textNo)
        {
            //チュートリアル
            case 0:
                csvFiles = "tutorial1";
                break;

            case 1:
                csvFiles = "tutorial2";
                break;

            //ストーリー    
            case 2:
                csvFiles = "story0-1";
                break;

            case 3:
                csvFiles = "story0-2";
                break;

            //ステージ1
            //双子天使の家　看板
            case 4:
                csvFiles = "stage1_kanban";
                break;

            //双子天使の家　尋ねる
            case 5:
                csvFiles = "stage1_house";
                break;

            //ゲート前の小屋　看板
            case 6:
                csvFiles = "stage1_KoyaKanban";
                break;
            
            //小屋フラグ回収前
            case 7:
                csvFiles = "stage1_Koya1";
                break;

            //小屋フラグ回収後
            //フラグ=先にゲートを調べる
            case 8:
                csvFiles = "stage1_Koya2";
                break;

            //ゲート　看板
            case 9:
                csvFiles = "stage1_GateKanban";
                break;

            //ゲート　フラグ回収前
            //ゲートを調べることで小屋のフラグ起動
            case 10:
                csvFiles = "stage1_Gate1";
                break;

            //ゲートフラグ回収後
            //フラグ=小屋でグレンと会話
            //ステージ2へ移動
            case 11:
                csvFiles = "stage1_Gate2";
                break;

            //ステージ2
            //天国ゲート行きの看板
            case 12:
                csvFiles = "stage2_GateHeaven";
                break;

            //冥界行きゲートの看板
            case 13:
                csvFiles = "stage2_KoyaKanban";
                break;

            //グレンとの会話(戦闘前)
            case 14:
                csvFiles = "stage2_Koya1"; ;
                break;

            //グレンとの会話(戦闘後)
            case 15:
                csvFiles = "stage2_Koya2";
                break;


            //ステージ3
            //天国ゲート行きの看板
            case 16:
                csvFiles = "stage3_Gate";
                break;

            //バー
            //入店
            case 17:
                csvFiles = "stage3_Bar1";
                break;

            //入口
            case 18:
                csvFiles = "stage3_1-1";
                break;

            //選択肢1「何が聞きたい？」
            case 19:
                csvFiles = "stage3_Bar2";
                break;

            //エリスについて
            case 20:
                csvFiles = "stage3_Bar2-1";
                break;

            //グレンについて
            case 21:
                csvFiles = "stage3_Bar2-1-2";
                break;

            //このバーについて
            case 22:
                csvFiles = "stage3_Bar2-1-3";
                break;

            //選択肢2「注文」
            //ピンクのカクテル飲んだ場合、フラグ起動
            case 23:
                csvFiles = "stage3_Bar2-2";
                break;

            //選択肢3「ばいばい」
            case 24:
                csvFiles = "stage3_Bar2-3";
                break;

            //グレン関連の話
            //カクテル飲んだ時のフラグが起動しているときに会話発生
            case 25:
                csvFiles = "stage3_BarAfter1";
                break;

            //選択肢1「いいやつ」
            case 26:
                csvFiles = "stage3_BarAfter1-1";
                break;

            //選択肢2「やなやつ」
            case 27:
                csvFiles = "stage3_BarAfter1-2";
                break;

            //選択肢3「わからない」
            case 28:
                csvFiles = "stage3_BarAfter1-3";
                break;

            //洞窟入り口
            case 29:
                csvFiles = "stage3_caveStart";
                break;

            //洞窟出口付近
            //再開グレン
            //ノーマルバージョン
            case 30:
                csvFiles = "stage3_caveEnd1-1";
                break;

            //感情極振りバージョン
            case 31:
                csvFiles = "stage3_caveEnd1-2";
                break;

            //グレン戦後　エリス登場
            case 32:
                csvFiles = "stage3_caveEnd2";
                break;

            //グレンに雷が落ちた後→VSエリス(負けイベ)
            case 33:
                csvFiles = "stage3_caveEnd3";
                break;

            //エリス戦後
            case 34:
                csvFiles = "stage3_caveEnd4";
                break;

            //ゴミ箱
            //ノーマル
            case 35:
                csvFiles = "DustBox_Normal";
                break;

            //怒り極振り
            case 36:
                csvFiles = "DustBox_Angry";
                break;

            //悲しみ極振り
            case 37:
                csvFiles = "DustBox_Sad";
                break;

            //感情極振り時の今後の調べるコマンドの時
            case 38:
                csvFiles = "AfterCheck";
                break;

            //エリス城前　看板
            case 39:
                csvFiles = "stage3_EllisCastleKanban";
                break;

            //グレンの家
            //鍵を持っていない場合
            case 40:
                csvFiles = "stage3_EllisCastleKoya1-1";
                break;

            //鍵を持っている場合
            case 41:
                csvFiles = "stage3_EllisCastleKoya1-2";
                break;

            //家の中　ピンクの小瓶
            case 42:
                csvFiles = "stage3_EllisCastleKoya2";
                break;


            //ステージ4
            //エリスの部屋手前　グレン戦
            case 43:
                csvFiles = "stage4_1";
                break;

            //グレン戦後
            //ピンクの小瓶を所持していないとき
            case 44:
                csvFiles = "stage4_2-1";
                break;

            //ピンクの小瓶を所持しているとき
            case 45:
                csvFiles = "stage4_2-2";
                break;

            //グレン消滅後
            //ピンクの小瓶を所持していないとき
            case 46:
                csvFiles = "stage4_getNoLove";
                break;

            //ピンクの小瓶を所持しているとき
            case 47:
                csvFiles = "stage4_getLove";
                break;

            //エリス戦開始
            //ノーマル時
            case 48:
                csvFiles = "stage4_VSEllis1";
                break;

            //怒り極振り時
            case 49:
                csvFiles = "stage4_VSEllis1_angry";
                break;

            //エリス戦第一形態突破
            case 50:
                csvFiles = "stage4_VSEllis2";
                break;

            //エリス戦第二形態突破
            case 51:
                csvFiles = "stage4_VSEllis3";
                break;

            //エリス討伐メッセージ
            case 52:
                csvFiles = "stage4_VSEllisEnd";
                break;

            //エリス戦終了後
            case 53:
                csvFiles = "stage4_End";
                break;


            //テスト用
            case 999:
                csvFiles = "testText2";
                break;

        }

        //TextSystemScript.SetTextPanel(message, charaName, charaIcon, LorR, anims);
        TextSystemScript.SetCSVPanel(csvFiles);

    }
}
