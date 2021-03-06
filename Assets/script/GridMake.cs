using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMake : MonoBehaviour
{
    public Vector2 gridWorldSize; // 전체 크기 
    public Tilemap tilemap;
    
    Vector3 bottomLeft;
    Vector3 topRight;

    public Node[,] gridArray; //  노드가 담길 이차원배열

    Ray camRay; // 스크린포인트에서 레이캐스트 쏘기 위해서
    RaycastHit2D rayhit; // 맞은 타일맵 가져올것임
    Camera cam; // 스크린포인트 레이캐스트 쓰기위함

    public GameObject groundPrefab; // 바닥 오브젝트 
    GameObject parentGrid; // 복사된 바닥의 부모 지정



    private void Start()
    {
        cam = GetComponent<Camera>();
        bottomLeft = tilemap.WorldToCell(tilemap.transform.position - tilemap.size / 2);
        topRight = tilemap.WorldToCell(tilemap.transform.position + tilemap.size / 2);
    }


    public void CreateGrid()       // 맨땅에 오브젝트 덩어리로 타일 만들고, 하나로 합치기 ( 짭타일맵 )
    {
        if (parentGrid != null)
            Destroy(parentGrid);
        parentGrid = new GameObject("parentGrid");

        gridArray = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];

        for (int x = 0; x < gridWorldSize.x; x++)
        {
            for (int y = 0; y < gridWorldSize.y; y++)
            {
                Vector2 startPos = Vector2.zero - new Vector2(gridWorldSize.x / 2.0f, gridWorldSize.y / 2.0f);
                Vector2 nodePos = startPos + new Vector2(x + 0.5f, y + 0.5f);
                gridArray[x, y] = new Node(true,x,y);
                GameObject obj = Instantiate(groundPrefab, startPos + new Vector2(x + 0.5f, y + 0.5f), Quaternion.Euler(0, 0, 0));
                obj.transform.parent = parentGrid.transform;
            }
        }
    }

    public void getGridFromTile() // 생성된 그리드에서 타일 받아오기
    {
        gridArray = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];
        
        for (int i = 0; i < gridWorldSize.x; i++)
        {
            for (int j = 0; j < gridWorldSize.y; j++)
            {
                bool walkable = true;
                
                //  Debug.Log(bottomLeft);
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f)) // 조그만 원을 움직이면서, 겹치는 타일 하나씩 찾음
                if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) walkable = true;
                gridArray[i, j] = new Node(walkable, i + (int)bottomLeft.x, j + (int)bottomLeft.y);
                Debug.Log(i + ", " + j + ", " + gridArray[i, j].gridX + ", "+ gridArray[i,j].gridY);
                Debug.Log("1");
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
                if (gridArray[checkX, checkY].walkable)
                    walkableUDLR[i] = true;
                neighbours.Add(gridArray[checkX, checkY]);
            }
        }
        //for(int i=0;i<neighbours.Count;i++)
        //    Debug.Log(neighbours[i].gridX+","+neighbours[i].gridY);

        return neighbours;
    }
}
