using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    protected Rigidbody2D rb;
    public GroundChecker gc;

    [SerializeField] protected float moveSpdX = 5;
    [SerializeField] protected float jumpPow = 10;

    [Range(-1, 1)] protected int inputLR = 0;
    protected bool inputJump = false;

    protected bool inputFlick = false;
    protected float flickDir = 0;
    protected float flickPow = 0;
    protected float flickTime = 0;

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

    }

    protected void FixedUpdate()
    {

        MoveControl();

    }

    protected virtual void InputControl()
    {

    }

    protected void MoveControl()
    {
        Vector3 moveSpd = rb.velocity;

        // フリック（最優先）
        if (flickTime >= 0)
        {
            flickTime -= Time.deltaTime;

            if (inputFlick)
            {
                Vector3 temp;
                temp.x = flickPow * Mathf.Cos(flickDir * Mathf.Deg2Rad);
                temp.y = flickPow * Mathf.Sin(flickDir * Mathf.Deg2Rad);
                moveSpd.x = temp.x > 0 && temp.x > moveSpd.x || temp.x < 0 && temp.x < moveSpd.x ? temp.x : moveSpd.x;
                moveSpd.y = temp.y > 0 && temp.y > moveSpd.y || temp.y < 0 && temp.y < moveSpd.y ? temp.y : moveSpd.y;
                inputFlick = false;
            }
        }
        else
        {
            // 通常移動
            moveSpd.x = inputLR * moveSpdX;

            //if (inputLR != 0)
            //{
            //    moveSpd.x = inputLR * moveSpdX;
            //    //if (gc.IsGround()) { moveSpd.x = inputLR * moveSpdX; }
            //}

            // ジャンプ
            if (inputJump)
            {
                moveSpd.y = jumpPow;
                inputJump = false;
            }
        }

        rb.velocity = moveSpd;
    }

    public int GetLR()
    {
        return inputLR;
    }

    public void InputLR(int _inputLR)
    {
        inputLR = _inputLR;
    }

    public void InputJump()
    {
        inputJump = true;
    }

    public void InputFlick(float dir, float pow, float time)
    {
        inputFlick = true;
        flickDir = dir;
        flickPow = pow;
        flickTime = time;
    }

    public void InputFlick(Vector3 pos, float pow, float time)
    {
        Vector3 posDis = pos - this.transform.position;
        float posDir = Mathf.Atan2(posDis.y, posDis.x) * Mathf.Rad2Deg;

        InputFlick(posDir, pow, time);
    }

    public void InputFlickStop()
    {
        flickTime = 0;

        Vector3 moveSpd = rb.velocity;
        moveSpd = Vector3.zero;
        rb.velocity = moveSpd;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
