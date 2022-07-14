using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridMake g;
    public aStar a;
    public GameObject grid;
    public Vector2 gridWorldSize;

    void Awake()
    {
        g = GetComponent<GridMake>();
        g.gridWorldSize = gridWorldSize; 
        a.gridCode = g;
        g.getGridFromTile();
        g.GetNeighbours(g.gridArray[0, 0]);
        //a.gridArray = g.gridArray;
        a.FindPath(g.gridArray[0,0],g.gridArray[(int)gridWorldSize.x-1, (int)gridWorldSize.y-1]);
        a.RetracePath(g.gridArray[0, 0], g.gridArray[(int)gridWorldSize.x-1, (int)gridWorldSize.y-1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
