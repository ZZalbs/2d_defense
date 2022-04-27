using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMake : MonoBehaviour
{

    public GameObject groundPrefab; // 바닥 오브젝트 
    GameObject parentGrid;
    public Vector2 gridWorldSize; // 노드 크기 

    Node[,] grid; //  노드가 담길 이차원배열

    public void CreateGrid()
    {
        if (parentGrid != null)
            Destroy(parentGrid);
        parentGrid = new GameObject("parentGrid");

        grid = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];
        for (int x = 0; x < gridWorldSize.x; x++)
        {
            for (int y = 0; y < gridWorldSize.y; y++)
            {
                Vector2 startPos = Vector2.zero - new Vector2(gridWorldSize.x / 2.0f, gridWorldSize.y / 2.0f);
                GameObject obj = Instantiate(groundPrefab, startPos + new Vector2(x + 0.5f, y + 0.5f), Quaternion.Euler(0, 0, 0));
                obj.transform.parent = parentGrid.transform;
                grid[x, y] = new Node(obj, true, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // 이 차 원 배 열
        bool[] walkableUDLR = new bool[4];

        //상하좌우의 노드 먼저 계산
        for (int i = 0; i < 4; i++)
        {
            int checkX = node.gridX + temp[i, 0];
            int checkY = node.gridY + temp[i, 1];
            if (checkX >= 0 && checkX < (int)gridWorldSize.x && checkY >= 0 && checkY < (int)gridWorldSize.y) // x,y가 월드 내 유효한 좌표면
            {
                if (grid[checkX, checkY].walkable)
                    walkableUDLR[i] = true;
                neighbours.Add(grid[checkX, checkY]);
            }
        }

        return neighbours;
    }
}
