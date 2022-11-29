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

    [SerializeField] private GameObject[] targetPool; // 풀 링할 타겟 설정

    GameObject[] playerBullet;
    [SerializeField] private GameObject[] turretA;
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
        turretA = new GameObject[100];
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
            playerBullet[i].SetActive(false);
        }

        for (int i = 0; i < turretA.Length; i++)
        {
            turretA[i] = Instantiate(turretAPrefab);
            turretA[i].SetActive(false);
        }

        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.001f);
        loading.SetActive(false);
    }

    public GameObject MakeObj(GameManager.Obj type)
    {

        switch (type)
        {
            case GameManager.Obj.enemyS:
                targetPool = enemyS;
                Debug.Log("enemyS Target Pool Set");
                break;
            case GameManager.Obj.enemyM:
                targetPool = enemyM;
                Debug.Log("enemyM Target Pool Set");
                break;
            case GameManager.Obj.enemyL:
                targetPool = enemyL;
                Debug.Log("enemyL Target Pool Set");
                break;
            case GameManager.Obj.turretA: // <- 철자 오류입니다, 철자오류가 일어나서 해당 Target Pool을 받지 않습니다 (enum 열거체를 사용해 이러한 부분을 방지합시다)
                targetPool = turretA;
                Debug.Log("Turret A Target Pool Set");
                break;
            case GameManager.Obj.playerBullet:
                targetPool = playerBullet;
                Debug.Log("playerBullet Target Pool Set");
                break;
        }

        for (int i = 0; i < targetPool.Length; i++) // 위 경우에서 Target Pool 설정이 제대로 되지 않았기에 기존 enemyS target Pool을 받아옵니다
        {
            if (!targetPool[i].activeSelf) // 그 중에서 비활성화된 놈을 찾을겁니다
            {
                targetPool[i].SetActive(true); // 찾았습니다, 자 이제 이놈을 활성화 시키면... 어디서 갑자기 새로운 적이 우상단에 나타나게 됩니다 (왜 우상단인지는 모르겠습니다)
                return targetPool[i];
            }
        }

        return null;
    }


}
