using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileFunc : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public Tilemap tilemap;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(pos);
        ///********************************************************
        /// 버그 위치
        Debug.Log(GameManager.instanceGM);

        Vector3 newPosition = new Vector3(cellPosition.x + (int)gridWorldSize.x / 2 +3, cellPosition.y + (int)gridWorldSize.y / 2 -1,0);
        Debug.Log(newPosition);
        //Debug.Log(cellPosition);
        if (GameManager.instanceGM.isSetWall) // is Set Wall이 True이면 -> 해당부분인거 싶단 말입니다
        {
            GameManager.instanceGM.tileFalse((int)newPosition.x, (int)newPosition.y);
            tilemap.SetTile(cellPosition, null); // <= 여기서 버그 발생하는줄 알았는데 아님
            //GameManager.instanceGM.isSetWall = false; <= 한번 테스트 해볼려고 제가 넣어봤습니다
            //Debug.Log("pos : "+newPosition);
        }

        
        if (GameManager.instanceGM.isSetWall && tilemap.GetTile(cellPosition)==null)
        {
            GameObject turretA = ObjectManager.instance.MakeObj(GameManager.Obj.turretA); // 우선 대충 수정했습니다, enum 열거체를 사용해 철자오류가 나도 바로 확인할 수 있도록 합시다
            Debug.Log("*****" + turretA.name + "*****");
            if (turretA != null)
            {
                turretA.transform.position = newPosition; // 여기서 우상단에 나타나는게 아닌가 싶습니다. (사실 newPosition이 tile index를 2차원배열처럼 반환하니 우상단 비율이 높을 뿐입니다, 실제로는 1사분면쪽에서 생성이 됩니다)
                TurretA code = turretA.GetComponent<TurretA>();
                Debug.Log(newPosition);
            }
            tilemap.SetTile(cellPosition, null);
            //Debug.Log("pos : "+newPosition);
        }
        
        
    }
}
