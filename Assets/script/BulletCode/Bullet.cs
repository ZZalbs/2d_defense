using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 dir = Vector3.zero;
    public float angle;
    public float size;
    public int dmg;

    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        
        
    }
    private void Update()
    {
        gameObject.transform.Translate(dir*speed*Time.deltaTime,Space.World);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy EnemyCode = collision.gameObject.GetComponent<Enemy>();
            EnemyCode.Onhit(dmg);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Wall")
            gameObject.SetActive(false);
    }
}
