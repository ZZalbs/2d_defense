using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretB : TurretInterface
{
    [SerializeField]List<GameObject> swords;
    public int swordCount;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.range);
    }

    void OnEnable()
    {
    }

    void Update()
    {
        

    }

    

    public override void Passive()
    {

    }

    public void ChangeSwordNum()
    {
        for (int i = 0; i < swordCount; i++)
        {
            Vector3 rotVec = Vector3.forward * 360 / swordCount * i;
            swords[i] = BulletManager.instanceBM.SwordMake(transform, rotVec,range, 100);
        }
    }


    //IEnumerator TurretMain()
    //{
    //    while (true)
    //    {
    //        shortestEnemy = TargetShortFind();
    //        if (shortestEnemy != null)
    //        {
    //            Attack(shortestEnemy);
    //            yield return new WaitForSeconds(this.attackDelay);
    //        }
    //    }
    //}


}
