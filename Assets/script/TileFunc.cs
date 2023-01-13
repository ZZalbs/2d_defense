using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileFunc : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public Tilemap upTilemap;
    public Tilemap thisTilemap;

    private void Start()
    {
        thisTilemap = this.GetComponent<Tilemap>();
    }

    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = upTilemap.WorldToCell(pos);
        Debug.Log(GameManager.instanceGM);

        Vector3 newPosition = new Vector3(cellPosition.x + (int)gridWorldSize.x / 2, cellPosition.y + (int)gridWorldSize.y / 2 -1,0);
        Debug.Log(newPosition);
        if (GameManager.instanceGM.isSetWall) 
        {
            GameManager.instanceGM.tileFalse((int)newPosition.x, (int)newPosition.y);
            upTilemap.SetTile(cellPosition, null); 
        }

        
        if (GameManager.instanceGM.isSetTurret && !GameManager.instanceGM.getTileTrue((int)newPosition.x, (int)newPosition.y))
        {
            GameObject turretA = ObjectManager.instance.MakeObj(ObjectManager.Obj.turretA); // 우선 대충 수정했습니다, enum 열거체를 사용해 철자오류가 나도 바로 확인할 수 있도록 합시다
            Debug.Log("*****" + turretA.name + "*****");
            if (turretA != null)
            {
                turretA.transform.position = GameManager.instanceGM.getTilePos((int)newPosition.x, (int)newPosition.y); 
                TurretA code = turretA.GetComponent<TurretA>();
            }
            upTilemap.SetTile(cellPosition, null);
        }
        
        
    }
}
