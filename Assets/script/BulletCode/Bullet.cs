using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 dir = Vector3.zero;
    public float size;

    private void Update()
    {
        gameObject.transform.Translate(dir*speed);
    }
}
