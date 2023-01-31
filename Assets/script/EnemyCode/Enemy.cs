using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    private int maxhp;
    public float speed;
    public int killgold;
    public List<Node> Path;
    [SerializeField] private Vector2[] pos; // 디버그용 타겟위치
    public aStar a;
    [SerializeField]int curPos=0;
    Vector3 targetPos;

    enum state {OnMove,OnEnd};
    state isEnd;

    void Start()
    {
        targetPos = Path[curPos].position; 
        pos = new Vector2[Path.Count];
        maxhp = hp;
        isEnd = state.OnMove;
    }

    void OnEnable()
    {
        GameManager.EnemyRetrace += changePath;// event 등록
        Path = GameManager.instanceGM.getDefaultPath();
        curPos = 0;
        targetPos = Path[curPos].position;
        //Debug.Log(gameObject.name+"in");
    }
    void OnDisable()
    {
        GameManager.EnemyRetrace -= changePath; // event 해제
        GameManager.instanceGM.EnemyDieCount();
        //Debug.Log(gameObject.name + "out");
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd == state.OnMove) NormalMove();
        else if (isEnd == state.OnEnd) EndMove();

        // 디버그용
        for (int i = 0; i < Path.Count; i++)
        {
            pos[i] = new Vector2(Path[i].gridX, Path[i].gridY);
            //Debug.Log(Path[i]);
            //Debug.Log(this.name + "(" + i.ToString() + ")" + " : " + pos[i]);
        }
    }

    void NormalMove()
    {
        Vector3 moveDir = (targetPos - gameObject.transform.position); // 움직임 위치 설정
        gameObject.transform.Translate(moveDir.normalized * Time.deltaTime * speed, Space.World);
        if (Vector3.Distance(gameObject.transform.position, targetPos) < 0.1f) // 타겟까지 거리가 0.1f 이내라면 -> 목표 위치에 도달
        {
            curPos++; // curPos++
            targetPos = Path[curPos].position; // 타겟 위치 수정
            if (curPos >= Path.Count - 1) // Path내 모든 위치를 이동했다면
            {
                isEnd = state.OnEnd;
            }
        }
    }

    void EndMove()
    {
        Vector3 moveDir = (Path[Path.Count - 1].position - gameObject.transform.position); // 움직임 위치 설정
        gameObject.transform.Translate(moveDir.normalized * Time.deltaTime * speed, Space.World);
        if (Vector3.Distance(gameObject.transform.position, Path[Path.Count - 1].position) < 0.05f)
        {
            curPos = 0;
            targetPos = Path[curPos].position;
            isEnd = state.OnMove;
            gameObject.SetActive(false); // 오브젝트 삭제
        }
    }



    public void Onhit(int dmg) // 데미지를 입는 경우
    {
        hp-=dmg;
        if (hp <= 0)
        {
            hp = maxhp;
            GameManager.instanceGM.GoldPlus(killgold);
            this.gameObject.SetActive(false);

        }
    }

    public void changePath()
    {
        if (this.gameObject.activeSelf)
        {
            a.FindPath(Path[curPos], Path[Path.Count - 1]); 
            Path = a.RetracePath(Path[curPos], Path[Path.Count - 1]);
            curPos = 0;
            targetPos = Path[curPos].position;
            Debug.Log(this.gameObject.name + "changed path");
        }

        
    }
}
