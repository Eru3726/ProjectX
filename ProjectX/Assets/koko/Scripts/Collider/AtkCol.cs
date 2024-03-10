using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCol : MonoBehaviour
{
    [SerializeField]
    public int damage = 10;

    [SerializeField]
    public float shock = 10;

    [SerializeField]
    public float inv = 1;

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
                iShock.TakeShock(shock, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z));
            }

            if (collision.TryGetComponent<IInvincible>(out IInvincible iInv))
            {
                iInv.TakeInv(inv);
            }
        }
    }
}
