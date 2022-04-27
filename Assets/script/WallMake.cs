using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMake : MonoBehaviour
{
    public void MakeWall()
    {
        StartCoroutine(MakeWallOnMouse());
    }

    IEnumerator MakeWallOnMouse()
    {
        if(Input.GetMouseButton(0))
        {

        }
        yield return null;
    }   
}
