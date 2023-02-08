using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public GameObject ground; // 노드를 저장하는 게임오브젝트
    public bool walkable;
    public bool isTurret; //터렛 깔렸는지 체크
    public int gridX;
    public int gridY;
    public Vector3 position;

    public bool start;
    public bool end;


    public Node parent;

    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Node(GameObject _ground, bool _walkable,bool _isTurret , int _gridX, int _gridY,Vector3 _position)
    {
        ground = _ground;
        walkable = _walkable;
        isTurret = _isTurret;
        gridX = _gridX;
        gridY = _gridY;
        position = _position;
    }
    public Node(bool _walkable, bool _isTurret, int _gridX, int _gridY, Vector2 _position)
    {
        walkable = _walkable;
        isTurret = _isTurret;
        gridX = _gridX;
        gridY = _gridY;
        position = _position;
    }
    public Node(bool _walkable, int _gridX, int _gridY)
    {
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
    }


    public void OnTurret()
    {
        isTurret = true;
    }
}
