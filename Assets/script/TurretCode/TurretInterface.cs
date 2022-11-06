using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInterface : MonoBehaviour
{
    public int damage;
    public float attackDelay;

    public virtual void TargetFind() { }
    public virtual void Attack() { }

}
