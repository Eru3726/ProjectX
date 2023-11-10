using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected GroundChecker gc;

    [SerializeField] protected float moveSpdX = 5;
    [SerializeField] protected float jumpPow = 10;

    protected int inputLR = 0;
    protected bool inputJump = false;

    protected virtual void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        gc = GetComponent<GroundChecker>();

        gc.InitCol();

    }

    protected virtual void Update()
    {

        gc.CheckGround();

        InputControl();

        Debug.Log(gc.GetFootPos());

    }

    protected void FixedUpdate()
    {

        MoveControl();

    }

    protected virtual void InputControl()
    {
        // 左右入力
        if (Input.GetKey(KeyCode.D)) { inputLR = 1; }
        else if (Input.GetKey(KeyCode.A)) { inputLR = -1; }
        else { inputLR = 0; }

        // キャラの向き変更
        if (inputLR != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * inputLR;
            transform.localScale = scale;
        }

        // 上入力
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (gc.IsGround())
            {
                inputJump = true;
                return;
            }
        }
    }

    protected void MoveControl()
    {
        Vector3 moveSpd = rb.velocity;

        // 通常移動
        if (inputLR != 0)
        {
            moveSpd.x = inputLR * moveSpdX;

            //if (gc.IsGround()) { moveSpd.x = inputLR * moveSpdX; }
        }

        // ジャンプ
        if (inputJump)
        {
            moveSpd.y = jumpPow;
            inputJump = false;
        }

        rb.velocity = moveSpd;
    }

    public void Flick(float dir, float pow)
    {
        Vector3 moveSpd = rb.velocity;
        Vector3 temp;
        temp.x = pow * Mathf.Cos(dir * Mathf.Deg2Rad);
        temp.y = pow * Mathf.Sin(dir * Mathf.Deg2Rad);
        moveSpd.x = temp.x > 0 && temp.x > moveSpd.x || temp.x < 0 && temp.x < moveSpd.x ? temp.x : moveSpd.x;
        moveSpd.y = temp.y > 0 && temp.y > moveSpd.y || temp.y < 0 && temp.y < moveSpd.y ? temp.y : moveSpd.y;
        rb.velocity = moveSpd;
    }

    public void Flick(Vector3 pos, float pow)
    {
        Vector3 posDis = pos - this.transform.position;
        float mouseDir = Mathf.Atan2(posDis.y, posDis.x) * Mathf.Rad2Deg;

        Flick(mouseDir, pow);
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
