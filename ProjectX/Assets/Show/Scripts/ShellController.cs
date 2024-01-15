using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] float ShellSpd = 5f;
    [SerializeField] float rotSpd = 10f;

    GameObject player;

    public EnemyController ec;

    bool ShellFlg = false;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    
    void FixedUpdate()
    {
       Vector3 direction = player.transform.position - transform.position;
       float degre = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

       Quaternion rotation = Quaternion.AngleAxis(degre, Vector3.forward);
       this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotSpd * Time.deltaTime);

       this.transform.Translate(Vector3.right * ShellSpd * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ec.ishoming = true;
            Destroy(gameObject);
        }
    }
}
