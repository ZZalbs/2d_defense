using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public List<Node> Path;
    public aStar a;
    [SerializeField]int curPos=0;
    Vector3 targetPos;

    void Start()
    {
        targetPos = Path[curPos].position;
        
    }

    void OnEnable()
    {
        GameManager.EnemyRetrace += changePath;
    }
    void OnDisable()
    {
        GameManager.EnemyRetrace -= changePath;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (targetPos - gameObject.transform.position);
        gameObject.transform.Translate(moveDir.normalized * Time.deltaTime*speed , Space.World);
        //Debug.Log(Path[curPos].gridX + "," + Path[curPos].gridY);
        if (Vector3.Distance(gameObject.transform.position, targetPos) <0.1f)
        {
            curPos++;
            targetPos = Path[curPos].position;
            if (curPos >= Path.Count - 1)
            {
                curPos = 0;
                targetPos = Path[curPos].position;
                gameObject.SetActive(false);
            }
        }
    }



    public void Onhit()
    {
        hp--;
        if (hp <= 0)
            this.gameObject.SetActive(false);
    }

    public void changePath()
    {
        Path = a.RetracePath(Path[curPos], Path[Path.Count - 1]);
        curPos = 0;
        targetPos = Path[curPos].position;
    }
}
