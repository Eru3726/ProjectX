using UnityEngine;

public class LineShot
{
    public void Shot(GameObject bullet, Transform myTransform, Transform playerTr, ElisData elisData)
    {
        ElisBullet eb = bullet.GetComponent<ElisBullet>();
        eb.attackPower = elisData.B_AttackPower;
        eb.moveTime = elisData.B_MoveTime;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = (playerTr.position - myTransform.position).normalized * elisData.B_MoveSpeed;
    }
}
