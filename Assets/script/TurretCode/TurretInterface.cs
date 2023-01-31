using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInterface : MonoBehaviour
{
    public int damage; // 딜
    public float fireRate; // 공속
    public float range; // 사거리
    public LayerMask lm; // 적 태그할 레이어마스크

    public virtual void TargetShortFind() { }
    public virtual void Attack(Transform enemy) { }

}
