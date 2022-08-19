using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public List<Node> Path;
    int curPos=0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (Path[curPos].position - gameObject.transform.position).normalized;
        gameObject.transform.Translate(moveDir*Time.deltaTime);
        Debug.Log(Path[curPos].gridX + "," + Path[curPos].gridY);
        if (gameObject.transform.position == Path[curPos].position)
        {
            curPos++;
            if (curPos > Path.Count)
                gameObject.SetActive(false);
        }
    }

    public void Onhit()
    {
        hp--;
        if (hp <= 0)
            this.gameObject.SetActive(false);
    }
}
