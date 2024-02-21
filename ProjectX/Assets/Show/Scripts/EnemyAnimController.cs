using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [SerializeField] GameObject Guren_GS;

    [SerializeField] Animator GurenAnim;

    [SerializeField] EnemyController ec;

    void Start()
    {
        GurenAnim = Guren_GS.GetComponent<Animator>();
    }


    void Update()
    {
       if(ec.FingerFlg)
       {
            Debug.Log("FingerAnim");
       }
       else if(ec.FingerSnapOnlyFlg)
       {
            GurenAnim.Play("Guren_FingerSnapOnlyAnimation");
       }
       else if(ec.DownFlg)
       {
            Debug.Log("DownAnim");
            GurenAnim.Play("Guren_GS_DownAnimation");
       }
       else
       {
            Debug.Log("NomalAnim");
            GurenAnim.Play("Guren_NomalAnimation");
       }
    }
}
