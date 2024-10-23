using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMake : MonoBehaviour
{
    

    public Vector2 gridWorldSize; // 전체 크기 
    public Tilemap tilemap; // uptilemap
    float tileSize;

    Vector3 tilePos;
    public Vector3 bottomLeft;
    public Vector3 topRight;

    [SerializeField]public Node[,] gridArray; //  노드가 담길 이차원배열

    Ray camRay; // 스크린포인트에서 레이캐스트 쏘기 위해서
    RaycastHit2D rayhit; // 맞은 타일맵 가져올것임
    Camera cam; // 스크린포인트 레이캐스트 쓰기위함

    public GameObject groundPrefab; // 바닥 오브젝트 
    GameObject parentGrid; // 복사된 바닥의 부모 지정

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < gridWorldSize.x; i++)
        {
            for (int j = 0; j < gridWorldSize.y; j++)
            {
                Gizmos.color = Color.white;
                if (!gridArray[i, j].walkable)
                    Gizmos.color = Color.blue;
                if (gridArray[i, j].isTurret)
                    Gizmos.color = Color.red;
                Gizmos.DrawSphere(gridArray[i, j].position, 0.1f);
                
            }
        }
    }

    void Awake()
    {

        tilePos = tilemap.transform.position;
        tileSize = tilemap.transform.localScale.x;
        Debug.Log(tilePos);
        cam = GetComponent<Camera>();
        bottomLeft = tilePos - tilemap.CellToWorld(tilemap.size / 2) + tilemap.transform.localScale/2; // 가장 끝 모서리가 아닌, 가장 끝 타일을 가져와야 하기 때문에 보정값(반타일)만큼 더해줌
        topRight = tilePos + tilemap.CellToWorld(tilemap.size / 2) - tilemap.transform.localScale / 2;
        /*
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);    //큐브 오브젝트 생성
        cube.transform.position = bottomLeft;

        
        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);    //큐브 오브젝트 생성
        cube2.transform.position = topRight;
    */
    }


    public void getGridFromTile(Vector3 scale) // 생성된 그리드에서 타일 받아오기
    {
        gridArray = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];
        
        for (int i = 0; i < gridWorldSize.x; i++)
        {
            for (int j = 0; j < gridWorldSize.y; j++)
            {
                bool walkable = true;
                Vector2 posVector = new Vector2((i * scale.x) + bottomLeft.x, (j * scale.y) + bottomLeft.y);
                foreach (Collider2D col in Physics2D.OverlapCircleAll(posVector, 0.2f)) // 조그만 원을 움직이면서, 겹치는 타일 하나씩 찾음
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer("platform")) { walkable = true; Debug.Log(i + "," + j); }
                }
                gridArray[i, j] = new Node(walkable,false, i,j, posVector);
            }
        }
    }


    public void TileFalse(int i,int j)
    {
        gridArray[i, j].walkable = false;
        Debug.Log("grid ("+ i+","+ j+ ") is false");
    }

    public void TileTrue(int i, int j)
    {
        gridArray[i, j].walkable = true;
    }

    public void TileTurret(int i, int j)
    {
        gridArray[i, j].isTurret = true;
    }

    public Vector3 GetTilePos(int i,int j)
    {
        return gridArray[i, j].position;
    }

    public bool GetTileTrue(int i,int j)
    {
        return gridArray[i, j].walkable;
    }

    public bool GetTileTurret(int i, int j)
    {
        return gridArray[i, j].isTurret;
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; // 이 차 원 배 열
        bool[] walkableUDLR = new bool[4];

        //상하좌우의 노드 먼저 계산
        for (int i = 0; i < 4; i++)
        {
            int indexX = node.gridX + temp[i, 0];
            int indexY = node.gridY + temp[i, 1];
            double posX = node.gridX + temp[i, 0]* tileSize;
            double posY = node.gridY + temp[i, 1]* tileSize;
            if (indexX >= 0 && indexX < (int)gridWorldSize.x && indexY >= 0 && indexY < (int)gridWorldSize.y) // x,y가 월드 내 유효한 좌표면
            {
                if (gridArray[indexX, indexY].walkable)
                    walkableUDLR[i] = true;
                neighbours.Add(gridArray[indexX, indexY]);
            }
        }
        //for(int i=0;i<neighbours.Count;i++)
        //    Debug.Log(neighbours[i].gridX+","+neighbours[i].gridY);

        return neighbours;
    }

}
