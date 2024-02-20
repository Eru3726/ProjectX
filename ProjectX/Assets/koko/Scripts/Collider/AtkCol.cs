using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCol : MonoBehaviour
{
    [SerializeField]
    int damage = 10;

    [SerializeField]
    float shock = 10;

    [SerializeField]
    float inv = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(this.gameObject.tag.ToString()))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable iDamage))
            {
                iDamage.TakeDamage(damage);
            }

            if (collision.TryGetComponent<IShockable>(out IShockable iShock))
            {
                iShock.TakeShock(shock, transform.position);
            }

            if (collision.TryGetComponent<IInvincible>(out IInvincible iInv))
            {
                iInv.TakeInv(inv);
            }
        }
    }
}
