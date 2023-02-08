using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speed;
    public Vector3 startPos;
    public int dmg;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(startPos,Vector3.forward,speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy EnemyCode = collision.gameObject.GetComponent<Enemy>();
            EnemyCode.Onhit(dmg);
        }
    }
}
