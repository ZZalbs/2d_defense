using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretA : TurretInterface
{
    void Start()
    {
        StartCoroutine(TurretMain());
    }

    void Update()
    {
        
    }

    public override void TargetFind ()
    {
       
    }

    IEnumerator TurretMain()
    {
        TargetFind();
        yield return new WaitForSeconds(this.attackDelay);
    }
}
