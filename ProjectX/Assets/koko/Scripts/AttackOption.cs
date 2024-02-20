using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOption : MonoBehaviour
{

    [SerializeField, Header("地形に当たったらDestroy")]
    protected bool groundDestroyFlag = false;

    [SerializeField, Header("生存時間オンオフ")]
    protected bool isLife = false;

    [SerializeField, Header("生存時間")]
    public float lifeCount = 2;

    [SerializeField, Header("攻撃が当たったら消えるか")]
    public bool isBullet = false;

    int delay = 0;

    int groundLayer = 3;

    private void FixedUpdate()
    {
        lifeCount -= Time.deltaTime;
        if (lifeCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer && groundDestroyFlag)
        {
            Destroy(this.gameObject);
        }

        if (!collision.gameObject.CompareTag(this.gameObject.tag.ToString()) && isBullet)
        {
            Destroy(this.gameObject);
        }
    }
}
