using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridMake a;
    void Start()
    {
        a.CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
