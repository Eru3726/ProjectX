using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSlimeController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField,Header("EnemyBulletPreアタッチしてね😊")] 
    GameObject EmBulletPre;

    [SerializeField, Header("animアタッチ")]
    GameObject slime;
    Animator anim;

    bool isShot = false;
    float animTime = 1;
    float animTimer = 0;

    [SerializeField, Header("発射間隔")]
    public float shotDelay = 1.0f;

    float timer = 0;

    private void Start()
    {
        anim = slime.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer>shotDelay)
        {
            timer = 0;
            isShot = true;
            Instantiate(EmBulletPre,transform.position, Quaternion.identity);
        }

        if (isShot)
        {
            animTimer += Time.deltaTime;
            if (animTimer > animTime)
            {
                animTimer = 0;
                isShot = false;
            }
            anim.Play("ShotSlime_ShotAnimation");
        }
        else
        {
            anim.Play("ShotSlime_NormalAnimation");
        }
    }
}

