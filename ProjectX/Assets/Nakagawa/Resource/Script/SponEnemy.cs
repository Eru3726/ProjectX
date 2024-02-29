using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponEnemy : MonoBehaviour
{
    public GameObject Normal;
    public GameObject Neeble;
    public GameObject Fly;
    GameObject EnemyN;
    GameObject EnemyNe;
    GameObject EnemyF;
    public GameObject effekeer;
    float timer = 0;
     bool effChek = true;
    float _ResponSpan = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > _ResponSpan)
        {
            EnemyN=Instantiate(Normal);
            EnemyN.transform.position=new Vector3(5, -3, 0);
            timer = 0;
        }
        if (timer > _ResponSpan - 4)
        {
            effChek = true;
        }
        if (effChek == true)
        {
            effekeer.SetActive(true);
        }
    }
}
