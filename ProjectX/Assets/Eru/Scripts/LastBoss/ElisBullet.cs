using UnityEngine;

public class ElisBullet : MonoBehaviour
{
    [HideInInspector]
    public int attackPower, moveTime;

    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveTime) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(attackPower);
            Destroy(this.gameObject);
        }
    }
}
