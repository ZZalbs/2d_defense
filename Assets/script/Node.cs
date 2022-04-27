using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public GameObject ground; // 노드를 저장하는 게임오브젝트
    public bool walkable;
    public int gridX;
    public int gridY;

    public bool start;
    public bool end;


    public Node parent;

    public int fCost;
    public int gCost;
    public int hCost;
    

    public Node(GameObject _ground, bool _walkable, int _gridX, int _gridY)
    {
        ground = _ground;
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
    }


}
