using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    // パブリック変数
    [Header("Groundを入れてね")]
    public LayerMask lm; // チェック用のレイヤー

    // 変数
    BoxCollider2D bc2d;          // ボックスコライダー2D

    bool isGround;              // 地面チェック用の変数

    // 初期化
    public void InitCol()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    // プレイヤーの中心位置（胸元）を取得
    public Vector3 GetCenterPos()
    {
        Vector3 pos = transform.position;
        // ボックスコライダーのオフセットから中心を計算
        pos.y += bc2d.offset.y;
        return pos;
    }

    // プレイヤーの足元座標を取得
    public Vector3 GetFootPos()
    {
        Vector3 pos = GetCenterPos();
        // ボックスコライダーのオフセットから中心を計算
        pos.y += -bc2d.size.y / 2;
        return pos;
    }

    // 地面に接しているかチェック
    public void CheckGround()
    {
        isGround = false;   // 一旦空中判定にしておく

        // デバッグ用に線を出す
        Vector3 foot = GetFootPos();    // 始点
        Vector3 len = Vector3.up * -1f; // 長さ
        float width = bc2d.size.x / 2;   // 当たり判定の幅

        // 左端、中央、右端の順にチェックしていく
        foot.x += -width;               // 初期位置を左にずらす

        for (int no = 0; no < 3; ++no)
        {
            RaycastHit2D result;        // 当たり判定の結果用の変数

            // レイを飛ばして、指定したレイヤーにぶつかるかチェック
            result = Physics2D.Linecast(foot, foot + len, lm);

            Debug.DrawLine(foot, foot + len, color:Color.red);      // デバッグ表示用

            // コライダーと接触したかチェック
            if (result.collider != null)
            {
                isGround = true;        // 地面と接触した
                // Debug.Log("地面と接触");
                return;                 // 終了
            }
            foot.x += width;
        }
        // Debug.Log("空中");
    }

    // 地面に接している変数を取得
    public bool IsGround() { return isGround; }
}
