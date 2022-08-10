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
        tilemap.SetTile(cellPosition, null);
        Vector2Int newPosition = new Vector2Int(cellPosition.x + (int)gridWorldSize.x / 2 +3, cellPosition.y + (int)gridWorldSize.y / 2 -1);
        Debug.Log("pos : "+newPosition);
    }
}
