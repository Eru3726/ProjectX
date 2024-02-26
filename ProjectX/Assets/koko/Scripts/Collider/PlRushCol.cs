using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlRushCol : MonoBehaviour
{
    [SerializeField, Header("プレイヤー本体アタッチ")]
    GameObject player;

    [SerializeField]
    int damage = 10;

    [SerializeField]
    float delay = 1;

    float count = 0;

    [SerializeField]
    List<GameObject> list = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(this.gameObject.tag.ToString()))
        {
            // collision.transform.parent.parent = this.transform.parent;

            if (collision.TryGetComponent<IDamageable>(out IDamageable iDamage))
            {
                list.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i] == collision.gameObject)
            {
                list.Remove(collision.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].TryGetComponent<IShockable>(out IShockable iShock))
            {
                iShock.TakeStop();
            }

            if (list[i].TryGetComponent<IDamageable>(out IDamageable iDamage))
            {
                if (count >= delay - 0.05f)
                {
                    iDamage.TakeDamage(damage);
                    count = 0;
                }
            }
        }

        count += Time.deltaTime;
    }
}
