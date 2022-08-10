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
        gameObject.transform.Translate(Path[curPos].position*Time.deltaTime);
        Debug.Log(Path[curPos].position);
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
