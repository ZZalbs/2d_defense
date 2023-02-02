using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class GameManager : MonoBehaviour
{
    public int wallGold; // 나중에 상수로 바꾸기
    public int turretGold;

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
    public Text lifeText;

    //빌드시간 알려주는 슬라이더
    public GameObject bar;
    public Slider buildTimeBar;
    public float currentTime;

    public bool isWaveTime = false; // wavetime이 true일때는 벽 건설 불가.
    public float buildTime = 10.0f; // 나중에 상수로 바꾸기

    int[] waveMonster = new int[] {10, 20, 30, 30, 30};
    int waveCount = 0;

    

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

    public bool isSetWall=false;
    public bool isSetTurret = false;
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
        Invoke("StartNewWave", buildTime);
        goldText.text = gold.ToString();
        lifeText.text = life.ToString();
    }

    private void Update()
    {
        BuildTimeBar();
        if (enemyCount == waveMonster[waveCount])
        {
            enemyCount = 0;
            isWaveTime = false;
            waveCount++;

            currentTime = 0;
            //bar.SetActive(true);

            Invoke("StartNewWave", buildTime);
        }
    }

    IEnumerator EnemySmake(int num)
    {
        enemyCount = 0;
        isWaveTime = true;
        for (int i=0;i<num; i++)
        {
            GameObject enemy = ObjectManager.instance.MakeObj(ObjectManager.Obj.enemyS);
            if (enemy != null)
            {
                enemy.transform.position = g.gridArray[0, 0].position;
                Enemy code = enemy.GetComponent<Enemy>();
                code.Path = defaultPath;
                code.a = a;
            }            
            yield return new WaitForSeconds(1.0f);
        }
        
    }

    void StartNewWave()
    {
        //bar.SetActive(false);
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

    public void tileFalse(int i,int j)
    {
        g.TileFalse(i, j);

        enemyReset();

        MakeDefaultPath();
    }

    public Vector3 getTilePos(int i,int j)
    {
        return g.GetTilePos(i, j);
    }

    public bool getTileTrue(int i, int j)
    {
        return g.GetTileTrue(i, j);
    }

    public void SetWall()
    {
        if (isSetTurret)
            isSetTurret = false;
        if (isSetWall)
            isSetWall = false;
        else
            isSetWall = true;
    }

    public void SetTurret()
    {
        if (isSetWall)
            isSetWall = false;

        if (isSetTurret)
            isSetTurret = false;
        else
            isSetTurret = true;
    }

    public void enemyReset()
    {
        if (EnemyRetrace != null)
            EnemyRetrace();
        else
            Debug.Log("Event ERROR!");
    }

    public void BuildTimeBar()
    {
        if(!isWaveTime)
            currentTime += Time.deltaTime;
        buildTimeBar.value = currentTime/buildTime;
    }

    public bool IsWallGold()
    {
        if (gold > wallGold) return true;
        else return false;
    }
    public bool IsTurretGold()
    {
        if (gold > turretGold) return true; 
        else return false;
    }

    public void WallGoldMinus()
    {
        gold -= wallGold;
        goldText.text = gold.ToString();
    }
    public void TurretGoldMinus()
    {
        gold -= turretGold;
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
        lifeText.text = life.ToString();
        if (life <= 0) GameOver();

    }

    void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}
