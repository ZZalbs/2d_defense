using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInterface : MonoBehaviour
{
    public int damage;
    public float attackDelay;
    public float range;
    public string enemyTag = "enemy";

    public virtual Transform TargetShortFind() { return null; }
    public virtual void Attack(Transform enemy) { }

}
