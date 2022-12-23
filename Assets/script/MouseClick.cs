using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    Vector3 mousePosition;
    public LayerMask platformLayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mousePosition,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D overCollider2d = Physics2D.OverlapCircle(mousePosition, 0.01f, platformLayer);
            
            if (overCollider2d!=null)
            {
                Debug.Log("hit"); 
                overCollider2d.transform.GetComponent<TileFunc>().MakeDot(mousePosition);
            }
        }
    }
}
