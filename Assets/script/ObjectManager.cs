using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance; // 싱글톤용
    public enum Obj { enemyS, enemyM, enemyL, turretA,turretB, playerBullet,sword }
    public GameObject loading;

    public GameObject playerBulletPrefab;
    public GameObject swordPrefab;
    public GameObject turretAPrefab;
    public GameObject turretBPrefab;
    public GameObject enemySPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemyLPrefab;

    [SerializeField] private GameObject[] targetPool; // 풀링할 타겟 설정

    GameObject[] playerBullet;
    GameObject[] sword;
    [SerializeField] private GameObject[] turretA;
    [SerializeField] private GameObject[] turretB;
    [SerializeField] private GameObject[] enemyS;
    GameObject[] enemyM;
    GameObject[] enemyL;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }

        playerBullet = new GameObject[100];
        sword = new GameObject[100];
        turretA = new GameObject[100];
        turretB = new GameObject[100];
        enemyS = new GameObject[50];
        enemyM = new GameObject[50];
        enemyL = new GameObject[50];
        
        StartCoroutine("Generate");
        DontDestroyOnLoad(gameObject);
    }
    IEnumerator Generate()
    {
        loading.SetActive(true);
        Time.timeScale = 0.0f;

        for (int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].name = "enemyS" + i;
            enemyS[i].SetActive(false);
        }

        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }

        for (int i = 0; i < enemyL.Length; i++)
        {
            enemyL[i] = Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }

        for (int i = 0; i < playerBullet.Length; i++)
        {
            playerBullet[i] = Instantiate(playerBulletPrefab);
            BulletManager.instanceBM.bulletCode[i] = playerBullet[i].GetComponent<Bullet>();
            playerBullet[i].SetActive(false);
        }

        for (int i = 0; i < sword.Length; i++)
        {
            sword[i] = Instantiate(swordPrefab);
            BulletManager.instanceBM.swordCode[i] = sword[i].GetComponent<Sword>();
            sword[i].SetActive(false);
        }

        for (int i = 0; i < turretA.Length; i++)
        {
            turretA[i] = Instantiate(turretAPrefab);
            turretA[i].SetActive(false);
        }

        for (int i = 0; i < turretB.Length; i++)
        {
            turretB[i] = Instantiate(turretBPrefab);
            turretB[i].SetActive(false);
        }

        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.001f);
        loading.SetActive(false);
    }

    public GameObject MakeObj(Obj type)
    {
        switch (type)
        {
            case Obj.enemyS:
                targetPool = enemyS;
                // Debug.Log("enemyS Target Pool Set");
                break;
            case Obj.enemyM:
                targetPool = enemyM;
                // Debug.Log("enemyM Target Pool Set");
                break;
            case Obj.enemyL:
                targetPool = enemyL;
                // Debug.Log("enemyL Target Pool Set");
                break;
            case Obj.turretA: 
                targetPool = turretA;
                // Debug.Log("Turret A Target Pool Set");
                break;
            case Obj.turretB:
                targetPool = turretB;
                // Debug.Log("Turret A Target Pool Set");
                break;
            case Obj.playerBullet:
                targetPool = playerBullet;
                // Debug.Log("playerBullet Target Pool Set");
                break;
            case Obj.sword:
                targetPool = sword;
                // Debug.Log("playerBullet Target Pool Set");
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf) 
            {
                targetPool[i].SetActive(true); 
                return targetPool[i];
            }
        }

        return null;
    }


}
