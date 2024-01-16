using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected GroundChecker gc;

    [SerializeField] protected bool moveX = true;
    [SerializeField] protected bool moveY = false;

    [SerializeField] public Vector2 moveSpd = new Vector2(5, 5);
    [SerializeField] public float jumpPow = 10;

    [Range(-1, 1)] protected int inputLR = 0;
    [Range(-1, 1)] protected int inputUD = 0;
    protected bool inputJump = false;

    protected bool inputFlick = false;
    protected float flickDir = 0;
    protected float flickPow = 0;
    protected float flickTime = 0;
    protected bool flickStop = false;

    protected virtual void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        gc = GetComponent<GroundChecker>();

        gc.InitCol();

    }

    protected virtual void Update()
    {

        gc.CheckGround();

    }

    protected void FixedUpdate()
    {

        MoveControl();

    }

    protected void MoveControl()
    {
        Vector3 moveVel = rb.velocity;

        // フリック（最優先）
        if (flickTime > 0)
        {
            flickTime -= Time.deltaTime;

            if (inputFlick)
            {
                Vector3 temp;
                temp.x = flickPow * Mathf.Cos(flickDir * Mathf.Deg2Rad);
                temp.y = flickPow * Mathf.Sin(flickDir * Mathf.Deg2Rad);
                moveVel.x = temp.x > 0 && temp.x > moveVel.x || temp.x < 0 && temp.x < moveVel.x ? temp.x : moveVel.x;
                moveVel.y = temp.y > 0 && temp.y > moveVel.y || temp.y < 0 && temp.y < moveVel.y ? temp.y : moveVel.y;
                inputFlick = false;
            }
        }
        else
        {
            // フリック使用後の慣性がない場合の処理
            if (flickStop)
            {
                InputFlickStop();
                flickStop = false;
            }

            // 横移動
            if (moveX)
            {
                moveVel.x = inputLR * moveSpd.x;
            }

            // 縦移動
            if (moveY)
            {
                moveVel.y = inputUD * moveSpd.y;
            }

            // ジャンプ
            if (inputJump)
            {
                moveVel.y = jumpPow;
                inputJump = false;
            }
        }

        rb.velocity = moveVel;
    }



    public int GetLR()
    {
        return inputLR;
    }

    public void InputLR(int _inputLR)
    {
        inputLR = _inputLR;
    }

    public void InputUD(int _inputUD)
    {
        inputUD = _inputUD;
    }

    public void InputJump()
    {
        inputJump = true;
    }

    public void InputFlick(float dir, float pow, float time)
    {
        flickDir = dir;
        flickPow = pow;
        flickTime = time;
        inputFlick = true;
    }

    public void InputFlick(float dir, float pow, float time, bool stop)
    {
        InputFlick(dir, pow, time);
        flickStop = stop;
    }

    public void InputFlick(Vector3 pos, float pow, float time)
    {
        Vector3 posDis = pos - this.transform.position;
        float posDir = Mathf.Atan2(posDis.y, posDis.x) * Mathf.Rad2Deg;
        InputFlick(posDir, pow, time);
    }

    public void InputFlick(Vector3 pos, float pow, float time, bool stop)
    {
        InputFlick(pos, pow, time);
        flickStop = stop;
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

    public bool IsGround()
    {
        return gc.IsGround();
    }
}
