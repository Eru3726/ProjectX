using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected GroundChecker gc;

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
        // 左右入力
        if (Input.GetKey(KeyCode.D)) { InputLR(1); }
        else if (Input.GetKey(KeyCode.A)) { InputLR(-1); }
        else { InputLR(0); }

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
                InputJump();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InputFlick(45, 10, 1);
        }
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
        }

        rb.velocity = moveSpd;
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

        //Vector3 moveSpd = rb.velocity;
        //Vector3 temp;
        //temp.x = pow * Mathf.Cos(dir * Mathf.Deg2Rad);
        //temp.y = pow * Mathf.Sin(dir * Mathf.Deg2Rad);
        //moveSpd.x = temp.x > 0 && temp.x > moveSpd.x || temp.x < 0 && temp.x < moveSpd.x ? temp.x : moveSpd.x;
        //moveSpd.y = temp.y > 0 && temp.y > moveSpd.y || temp.y < 0 && temp.y < moveSpd.y ? temp.y : moveSpd.y;
        //rb.velocity = moveSpd;
    }

    public void Flick(Vector3 pos, float pow, float time)
    {
        Vector3 posDis = pos - this.transform.position;
        float mouseDir = Mathf.Atan2(posDis.y, posDis.x) * Mathf.Rad2Deg;

        InputFlick(mouseDir, pow, time);
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
