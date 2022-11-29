using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridMake g;
    public aStar a;
    public GameObject grid;
    public Vector2 gridWorldSize;
    List<Node> enemyPath;
    public GameObject startPos;

    public delegate void EnemyPathHandler(); 
    public static event EnemyPathHandler EnemyRetrace;

    public enum Obj {enemyS,enemyM,enemyL,turretA,playerBullet}
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

        g = GetComponent<GridMake>(); 
        g.gridWorldSize = gridWorldSize;
        a.gridCode = g; // a* algorithm에 값 입력
    }

    void Start()
    {
        g.getGridFromTile();
        //a.gridArray = g.gridArray;

        a.FindPath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]); // 처음지점부터 마지막 지점에 대한 Path 탐색

        enemyPath = a.RetracePath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]); // Path Node 정보 반환
        //EnemyRetrace();
        StartCoroutine(EnemySmake());
    }

    IEnumerator EnemySmake()
    {
        while (true)
        {
            GameObject enemy = ObjectManager.instance.MakeObj(Obj.enemyS);
            if (enemy != null)
            {
                enemy.transform.position = startPos.transform.position;
                Enemy code = enemy.GetComponent<Enemy>();
                code.Path = enemyPath; // Path를 받아주지만 비활성화된 Tile에 Path는 받아주지 않습니다
                code.a = a;
            }
            
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void tileFalse(int i,int j)
    {
        g.TileFalse(i,j);
        
        enemyReset();

        a.FindPath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]); 
        enemyPath = a.RetracePath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]); 
        // 실제로 바꿔보니 이제 기존 TileFalse를 할 때 비활성화 되있던 Enemy도 False된 Tile을 경로에 잘 반영합니다
    }

    public void setWall()
    {
        if(isSetWall)
            isSetWall = false;
        else
            isSetWall = true;
    }

    public void setTurret()
    {
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
}
