﻿using System.Collections;
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

    public override Transform TargetFind ()
    {
        GameObject[] searchedEnemy = GameObject.FindGameObjectsWithTag(enemyTag); // 적 콜라이더
        

        Transform shortestEnemy = null;
        if (searchedEnemy.Length > 0)
        {
            float shortDistance = Mathf.Infinity; // 기준은 최댓값에서 시작
            foreach (GameObject enemyObject in searchedEnemy) // 콜라이더 값 쭉 찾기.
            {
                float distance = Vector2.SqrMagnitude(transform.position - enemyObject.transform.position); // 거리 = 거리계산함수(벡터값의 차)
                if (shortDistance > distance) // 거리가 최솟값 거리보다 작으면
                {
                    shortDistance = distance; // 최솟값을 지금의 거리로 바꾸고
                    shortestEnemy = enemyObject.transform; // 이번 콜라이더가 가장 가까운 적
                }
            }
        }
        return shortestEnemy;
    }

    public override void Attack(Transform enemy) 
    {
        Debug.Log("attack to "+ enemy.position);
    }


    IEnumerator TurretMain()
    {
        Attack(TargetFind());
        yield return new WaitForSeconds(this.attackDelay);
    }

    
}