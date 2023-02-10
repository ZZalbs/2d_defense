using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public partial class GameManager : MonoBehaviour
{
    public int wallGold; // 나중에 상수로 바꾸기
    public int[] turretGold;

    enum back {wall,turA,turB};
    public GameObject[] backgrounds;
    public enum Turret { turretA,turretB }
    GridMake g;
    public aStar a;
    public GameObject grid;
    public Vector2 gridWorldSize;
    List<Node> defaultPath;

    public delegate void EnemyPathHandler();
    public static event EnemyPathHandler EnemyRetrace;

    public int enemyCount; // 웨이브 끝 판단용 적 죽은 숫자 세기

    public int gold;
    public Text goldText;
    int life;
    public GameObject[] lifeUI;


    //빌드시간 알려주는 슬라이더
    public GameObject bar;
    public Slider buildTimeBar;
    public float currentTime;

    public bool isWaveTime = false; // wavetime이 true일때는 벽 건설 불가.
    public float buildTime; // 나중에 상수로 바꾸기

    int[] waveHP = new int[] { 5, 10, 20, 30, 50 };
    int[] waveMonster = new int[] { 10, 20, 30, 30, 30 };
    int waveCount = 0;
    enum gameState { clear,over};
    public GameObject EndingUI;
    public GameObject EndingTile;
    public Text EndingText;
    float timecount;
    public GameObject skipButton;

    //private static GameManager _instanceGM;
    public static GameManager instanceGM;
    //{
    //    get
    //    {
    //        if (!_instanceGM)
    //        {
    //            _instanceGM = GameObject.FindObjectOfType(typeof(GameManager));
    //            return _instanceGM;
    //        }
    //    }
    //}

    public bool isSetWall = false;
    public bool isSetTurret = false;
    public bool isSetTurretB = false;
    void Awake()
    {
        if (instanceGM != this)
        {
            instanceGM = this;
        }

        // 게임 시작 기본설정
        gold = 30;
        life = 5;
        /////

        g = GetComponent<GridMake>();
        g.gridWorldSize = gridWorldSize;
        a.gridCode = g; // a* algorithm에 값 입력
    }

    void Start()
    {
        g.getGridFromTile(grid.transform.localScale);
        //a.gridArray = g.gridArray;

        a.FindPath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]); // 처음지점부터 마지막 지점에 대한 Path 탐색

        defaultPath = a.RetracePath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]); // Path Node 정보 반환
        //EnemyRetrace();
        StartCoroutine("StartNewWave");
        goldText.text = gold.ToString();
        EndingTile.SetActive(false);
        EndingUI.SetActive(false);
    }

    private void Update()
    {
        
        if (enemyCount == waveMonster[waveCount])
        {
            enemyCount = 0;
            isWaveTime = false;
            waveCount++;
            if (waveCount >= waveMonster.Length)
                GameEnd(gameState.clear);
            currentTime = 0;
            //bar.SetActive(true);

            StartCoroutine("StartNewWave");
        }
    }

    IEnumerator EnemySmake(int num)
    {
        enemyCount = 0;
        isWaveTime = true;
        skipButton.SetActive(false);
        for (int i = 0; i < num; i++)
        {
            GameObject enemy = ObjectManager.instance.MakeObj(ObjectManager.Obj.enemyS);
            if (enemy != null)
            {
                enemy.transform.position = g.gridArray[0, 0].position;
                Enemy code = enemy.GetComponent<Enemy>();
                code.hp = waveHP[waveCount];
                code.Path = defaultPath;
                code.a = a;
            }
            yield return new WaitForSeconds(1.0f);
        }

    }

    IEnumerator StartNewWave()
    {
        timecount=0;
        skipButton.SetActive(true);
        while(timecount<=buildTime)
        {
            BuildTimeBar(timecount);
            timecount += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StartCoroutine(EnemySmake(waveMonster[waveCount]));
    }


    //PATH
    public void MakeDefaultPath()
    {
        a.FindPath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]);
        defaultPath = a.RetracePath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]);
    }

    public List<Node> getDefaultPath()
    {
        return defaultPath;
    }

    public void tileFalse(int i, int j)
    {
        g.TileFalse(i, j);

        enemyReset();

        MakeDefaultPath();
    }

    public void TileTurret(int i, int j) // 터렛 지은거 자료구조에 반영
    {
        g.TileTurret(i, j);
    }


    public Vector3 getTilePos(int i, int j)
    {
        return g.GetTilePos(i, j);
    }

    public bool getTileTrue(int i, int j)
    {
        return g.GetTileTrue(i, j);
    }

    public bool GetTileTurret(int i,int j)
    {
        return g.GetTileTurret(i, j);
    }


    public void SetWall()
    {
        if (isSetTurret || isSetTurretB)
        {
            isSetTurret = false;
            isSetTurretB = false;
        }
        if (isSetWall)
            isSetWall = false;
        else { 
            isSetWall = true;
        }
    }

    public void SetTurret()
    {
        if (isSetWall || isSetTurretB)
        {
            isSetWall = false;
            isSetTurretB = false;
        }
        if (isSetTurret)
            isSetTurret = false;
        else
        {
            isSetTurret = true;
        }
    }

    public void SetTurretB()
    {
        if (isSetWall || isSetTurret)
        {
            isSetWall = false;
            isSetTurret = false;
        }
        if (isSetTurretB)
            isSetTurretB = false;
        else
        {
            isSetTurretB = true;
        }
    }

    public void enemyReset()
    {
        if (EnemyRetrace != null)
            EnemyRetrace();
        else
            Debug.Log("Event ERROR!");
    }

    public void BuildTimeBar(float timeCount)
    {
        if (!isWaveTime)
            currentTime = timeCount;
        buildTimeBar.value = currentTime / buildTime;
    }

    public void Skip()
    {
        buildTimeBar.value = 1;
        timecount += 100;
    }

    public bool IsWallGold()
    {
        if (gold >= wallGold) return true;
        else return false;
    }
    public bool IsTurretGold(Turret i)
    {
        if (gold >= turretGold[(int)i]) return true;
        else return false;
    }

    public void WallGoldMinus()
    {
        gold -= wallGold;
        goldText.text = gold.ToString();
    }
    public void TurretGoldMinus(Turret i)
    {
        gold -= turretGold[(int)i];
        goldText.text = gold.ToString();
    }

    public void GoldPlus(int getgold)
    {
        gold += getgold;
        goldText.text = gold.ToString();
    }

    public void EnemyDieCount()
    {
        enemyCount++;
    }

    public void LifeMinus()
    {
        life--;
        lifeUI[life].SetActive(false);
        if (life <= 0) GameEnd(gameState.over);

    }

    //public void resetBackColor()
    //{
    //    for(int i=0;i<3;i++)
    //    {
    //        SpriteRenderer s = backgrounds[i].GetComponent<SpriteRenderer>();
    //        s.color = new Color(180, 143, 100);
    //    }
    //}

    //public void ChangeBackColor(GameObject g)
    //{
    //    SpriteRenderer s = g.GetComponent<SpriteRenderer>();
    //    s.color = new Color(123, 99, 71);
    //}

    void GameEnd(gameState g)
    {
        EndingTile.SetActive(true);
        if(g==gameState.clear)
        {
            EndingText.text = "Congratulations!";
        }
        else if (g == gameState.over)
        {
            EndingText.text = "Game Over! \n you failed in" + waveCount + " wave";
        }
        EndingUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
