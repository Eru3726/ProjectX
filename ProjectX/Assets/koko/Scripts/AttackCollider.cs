using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField, Header("ダメージ")]
    public int dmg = 5;

    [SerializeField, Header("衝撃")]
    public float shock = 5;

    [SerializeField, Header("無敵時間")]
    public float inv = 1;

    [SerializeField, Header("衝撃座標は親オブジェか")]
    public bool apFlag = false;

    [HideInInspector]
    public Vector3 apPos;

    [SerializeField, Header("属性")]
    public StageData.ATK_DATA atkType;

    [SerializeField, Header("攻撃レイヤー")]
    public StageData.LAYER_DATA atkLayer;

    [SerializeField, Header("地形に当たったらDestroy")]
    protected bool groundDestroyFlag = false;

    [SerializeField, Header("生存時間オンオフ")]
    protected bool isLife = true;

    [SerializeField, Header("生存時間")]
    public float lifeCount = 2;

    [SerializeField, Header("攻撃が当たったら消えるか")]
    public bool isBullet = false;

    private void FixedUpdate()
    {
        if (isLife) { UpdateLife(); }

        if (apFlag)
        {
            apPos = transform.parent.position;
        }
        else
        {
            apPos = this.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && groundDestroyFlag)
        {
            Destroy(this.gameObject);
        }

        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            //damageable.TakeDamage();
        }
    }

    protected void UpdateLife()
    {
        lifeCount -= Time.deltaTime;
        if (lifeCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
