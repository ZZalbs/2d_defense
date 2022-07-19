using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance; // 싱글톤용

    public GameObject loading;

    public GameObject playerBulletPrefab;
    public GameObject turretAPrefab;
    public GameObject enemySPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemyLPrefab;
    public GameObject wallPrefab;
    GameObject[] targetPool; // 풀 링할 타겟 설정

    GameObject[] playerBullet;
    GameObject[] turretA;
    GameObject[] enemyS;
    GameObject[] enemyM;
    GameObject[] enemyL;
    GameObject[] wall;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }

        playerBullet = new GameObject[100];
        turretA = new GameObject[100];
        enemyS = new GameObject[50];
        enemyM = new GameObject[50];
        enemyL = new GameObject[50];
        wall = new GameObject[50];
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

        for (int i = 0; i < turretA.Length; i++)
        {
            turretA[i] = Instantiate(turretAPrefab);
            turretA[i].SetActive(false);
        }

        for (int i = 0; i < playerBullet.Length; i++)
        {
            playerBullet[i] = Instantiate(playerBulletPrefab);
            playerBullet[i].SetActive(false);
        }

        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.001f);
        loading.SetActive(false);
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "enemyS":
                targetPool = enemyS;
                break;
            case "enemyM":
                targetPool = enemyM;
                break;
            case "enemyL":
                targetPool = enemyL;
                break;
            case "turretA":
                targetPool = turretA;
                break;
            case "playerBullet":
                targetPool = playerBullet;
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
