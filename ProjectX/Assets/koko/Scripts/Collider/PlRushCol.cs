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

    float counter = 0;

    [SerializeField]
    List<GameObject> targetList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(this.gameObject.tag.ToString()))
        {
            // collision.transform.parent.parent = this.transform.parent;

            if (collision.TryGetComponent<IDamageable>(out IDamageable iDamage))
            {
                targetList.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if(targetList[i] == collision.gameObject)
            {
                targetList.Remove(collision.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].TryGetComponent<IShockable>(out IShockable iShock))
            {
                iShock.TakeStop();
            }

            if (targetList[i].TryGetComponent<IDamageable>(out IDamageable iDamage))
            {
                if (counter >= delay - 0.05f)
                {
                    iDamage.TakeDamage(damage);
                }
            }
        }

        counter += Time.deltaTime;

        if (counter > delay)
        {
            counter = 0;
        }
    }
}
