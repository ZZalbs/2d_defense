using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTestRay : MonoBehaviour
{
    public Tilemap tilemap;

    //마우스가 타일 위에 위치할 때만 작업할 것이기 때문에 onMouseOver를 사용했습니다.

    //가능하면 기즈모로 하는것도 좋을것 같네요.

    private void Update()
    {
        //onMouseOver();
    }

    private void onMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);



        if (this.tilemap = hit.transform.GetComponent<Tilemap>())
        {
            this.tilemap.RefreshAllTiles();

            int x, y;
            x = this.tilemap.WorldToCell(ray.origin).x;
            y = this.tilemap.WorldToCell(ray.origin).y;

            Vector3Int v3Int = new Vector3Int(x, y, 0);
            Debug.Log(v3Int);

        }
        
    }
    private void onMouseExit()
    {
        this.tilemap.RefreshAllTiles();

    }
}
