using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField, Header("ダメージ")]
    public float dmg = 5;

    [SerializeField, Header("衝撃")]
    public float shock = 5;

    [SerializeField, Header("衝撃座標は親オブジェか")]
    public bool meleeFlag = false;

    [SerializeField, Header("属性")]
    public StageData.ATK_DATA atkType;

    [SerializeField, Header("攻撃レイヤー")]
    public StageData.LAYER_DATA atkLayer;

    [SerializeField, Header("地形に当たったらDestroy")]
    protected bool groundDestroyFlag = false;

    [SerializeField, Header("生存時間オンオフ")]
    protected bool lifeFlag = true;

    [SerializeField, Header("生存時間")]
    public float lifeTime = 2;

    private void FixedUpdate()
    {
        if (lifeFlag) { UpdateLife(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && groundDestroyFlag)
        {
            Destroy(this.gameObject);
        }
    }

    protected void UpdateLife()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
