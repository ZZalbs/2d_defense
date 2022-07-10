using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridMake g;
    public aStar a;
    public GameObject grid;

    void Awake()
    {
        //스타트노드, 엔드노드 지정하는 코드 필요


        g.getGridFromTile();
        a.gridArray = g.gridArray;
        a.FindPath(start,end);
        a.RetracePath(start, end);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
