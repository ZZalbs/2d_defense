using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretA : TurretInterface
{

    [SerializeField]Transform targetEnemy = null;
    
    float currentFireRate;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, this.range);
    }

    void Start()
    {
        currentFireRate = 0;
        InvokeRepeating("TargetShortFind", 0f, this.fireRate);
    }

    void Update()
    {
        if(targetEnemy != null)
        {
            currentFireRate -= Time.deltaTime;
            if (currentFireRate <= 0)
            {
                currentFireRate = this.fireRate;
                Attack(targetEnemy);
            }
        }
        
    }

    public override void TargetShortFind()
    {

        Collider2D[] searchedEnemy = Physics2D.OverlapCircleAll(transform.position, range, lm);
        //GameObject[] searchedEnemy = GameObject.FindGameObjectsWithTag(enemyTag); // 적 콜라이더

        if (searchedEnemy.Length > 0)
        {
            float shortDistance = Mathf.Infinity; // 기준은 최댓값에서 시작
            foreach (Collider2D enemyObject in searchedEnemy) // 콜라이더 값 쭉 찾기.
            {
                float distance = Vector2.SqrMagnitude(transform.position - enemyObject.transform.position); // 거리 = 거리계산함수(벡터값의 차)
                if (shortDistance > distance) // 거리가 최솟값 거리보다 작으면
                {
                    shortDistance = distance; // 최솟값을 지금의 거리로 바꾸고
                    targetEnemy = enemyObject.transform; // 이번 콜라이더가 가장 가까운 적
                }
            }
        }
        else { targetEnemy = null; }
    }



    public override void Attack(Transform enemy) 
    {
        
            Vector3 dir = (enemy.position - gameObject.transform.position).normalized;
            BulletManager.instanceBM.BulletLinearShoot(gameObject.transform.position, dir, 50);
            Debug.Log("attack to " + enemy.position);
        
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
