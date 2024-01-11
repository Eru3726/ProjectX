using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] float ShellSpd = 5f;
    [SerializeField] float rotSpd = 300f;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    
    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        float degre = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(degre,Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotSpd * Time.deltaTime);

        Debug.Log(degre);
        this.transform.Translate(Vector3.right * ShellSpd * Time.deltaTime);
    }
}
