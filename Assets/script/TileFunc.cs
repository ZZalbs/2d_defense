using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileFunc : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public Tilemap upTilemap;
    public Tilemap thisTilemap;
    public TileBase newTile;

    private void Start()
    {
        gridWorldSize = GameManager.instanceGM.gridWorldSize;
        thisTilemap = this.GetComponent<Tilemap>();
    }

    public void MakeDot(Vector3 pos)
    {
        Vector3Int cellPosition = upTilemap.WorldToCell(pos);
        cellPosition.z = (int)upTilemap.transform.position.z;
        Debug.Log("make false"+cellPosition);
        Vector3Int newPosition = new Vector3Int(cellPosition.x + (int)gridWorldSize.x / 2, cellPosition.y + (int)gridWorldSize.y / 2 -1,0);
        Debug.Log(newPosition);
        if (GameManager.instanceGM.isSetWall&&GameManager.instanceGM.IsWallGold() && !GameManager.instanceGM.isWaveTime)  // 벽 설치
        {
            upTilemap.SetTile(cellPosition, newTile);
            //upTilemap.RefreshTile(cellPosition);
            GameManager.instanceGM.WallGoldMinus();
            GameManager.instanceGM.tileFalse((int)newPosition.x, (int)newPosition.y);
        }

        // 터렛 설치
        else if (GameManager.instanceGM.isSetTurret && !GameManager.instanceGM.getTileTrue((int)newPosition.x, (int)newPosition.y) && GameManager.instanceGM.IsTurretGold())
        {
            GameObject turretA = ObjectManager.instance.MakeObj(ObjectManager.Obj.turretA); 
            Debug.Log("*****" + turretA.name + "*****");
            if (turretA != null)
            {
                turretA.transform.position = GameManager.instanceGM.getTilePos((int)newPosition.x, (int)newPosition.y);
                GameManager.instanceGM.TurretGoldMinus();
                TurretA code = turretA.GetComponent<TurretA>();
            }
        }



        
    }
}
