using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAttackCollider : MonoBehaviour
{
    [SerializeField, Header("ダメージ")]
    public int dmg = 5;

    [SerializeField, Header("衝撃")]
    public float shock = 5;

    [SerializeField, Header("無敵時間")]
    public float inv = 1;

    [SerializeField, Header("衝撃座標は親オブジェか(衝撃方向に影響)")]
    public bool apFlag = false;

    [HideInInspector]
    public Vector3 apPos;

    [SerializeField, Header("属性")]
    public StageData.ATK_DATA atkType;

    [SerializeField, Header("攻撃レイヤー")]
    public StageData.LAYER_DATA atkLayer;

    private void FixedUpdate()
    {
        if (apFlag)
        {
            apPos = transform.parent.position;
        }
        else
        {
            apPos = this.transform.position;
        }
    }
}
