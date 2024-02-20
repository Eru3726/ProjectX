using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlRushCol : MonoBehaviour
{
    [SerializeField, Header("プレイヤー本体アタッチ")]
    GameObject player;

    [SerializeField]
    int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(this.gameObject.tag.ToString()))
        {
            collision.transform.parent.parent = this.transform.parent;

            if (collision.TryGetComponent<IDamageable>(out IDamageable iDamage))
            {
                iDamage.TakeDamage(damage);
            }
        }
    }
}
