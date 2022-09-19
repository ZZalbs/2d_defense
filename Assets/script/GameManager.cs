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

    public static GameManager instanceGM;

    void Awake()
    {
        g = GetComponent<GridMake>();
        g.gridWorldSize = gridWorldSize;
        a.gridCode = g;
    }

    void Start()
    {
        g.getGridFromTile();
        //a.gridArray = g.gridArray;

        a.FindPath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]);

        enemyPath = a.RetracePath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x - 1, (int)gridWorldSize.y - 1]);
        //EnemyRetrace();
        StartCoroutine(EnemySmake());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySmake()
    {
        while (true)
        {
            
            GameObject enemy = ObjectManager.instance.MakeObj("enemyS");
            if (enemy != null)
            {
                enemy.transform.position = startPos.transform.position;
                Enemy code = enemy.GetComponent<Enemy>();
                code.Path = enemyPath;
                code.a = a;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void tileFalse(int i,int j)
    {
        g.TileFalse(i,j);
    }

    public void enemyReset()
    {
        if (EnemyRetrace != null)
            EnemyRetrace();
        else
            Debug.Log("Event ERROR!");
    }
}
