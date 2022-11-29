using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public List<Node> Path; // Path는 Findpath를 제외하면 어디서 받아옴???
    [SerializeField] private Vector2[] pos; // 디버그용으로 존재하는듯 -> 타겟의 위치를 직접 확인하는 용도
    public aStar a;
    [SerializeField]int curPos=0;
    Vector3 targetPos;

    void Start()
    {
        targetPos = Path[curPos].position; 
        pos = new Vector2[Path.Count];
    }

    void OnEnable()
    {
        GameManager.EnemyRetrace += changePath;// event 등록
        //Debug.Log(gameObject.name+"in");
    }
    void OnDisable()
    {
        GameManager.EnemyRetrace -= changePath; // event 해제
        //Debug.Log(gameObject.name + "out");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (targetPos - gameObject.transform.position); // 움직임 위치 설정
        gameObject.transform.Translate(moveDir.normalized * Time.deltaTime*speed , Space.World); // 실제로 움직임
        if (Vector3.Distance(gameObject.transform.position, targetPos) <0.1f) // 타겟가 현재 오브젝트 위치가 0.1f 이내라면 -> 목표 위치에 도달
        {
            curPos++; // curPos++
            targetPos = Path[curPos].position; // 타겟 위치 수정
            if (curPos >= Path.Count - 1) // Path내 모든 위치를 이동했다면
            {
                curPos = 0; 
                targetPos = Path[curPos].position;
                gameObject.SetActive(false); // 값을 초기화 한 뒤 오브젝트 비활성화
            }
        }


        // 디버그용
        for (int i = 0; i < Path.Count; i++)
        {
            pos[i] = new Vector2(Path[i].gridX, Path[i].gridY); // pos에다가 path grid 정보 설정
            //Debug.Log(Path[i]);
            //Debug.Log(this.name + "(" + i.ToString() + ")" + " : " + pos[i]);
        }
    }



    public void Onhit() // 데미지를 입는 경우
    {
        hp--;
        if (hp <= 0)
            this.gameObject.SetActive(false);
    }

    public void changePath() // 오브젝트 Path 변경, 버그로 생성된 자식은 해당 방식으로 Path를 받아오지 못함
    {
        if (this.gameObject.activeSelf) // 활성화되있으면 changePath를 합니다 -> 비활성화된 자식들은 이걸 통해서 Path가 수정되지 않습니다
        {
            a.FindPath(Path[curPos], Path[Path.Count - 1]); //
            Path = a.RetracePath(Path[curPos], Path[Path.Count - 1]); // 현재위치에서 마지막 위치로 Path 반환
            curPos = 0;
            targetPos = Path[curPos].position;
            Debug.Log(this.gameObject.name + "changed path");
        }

        // 위 조건분기가 없어도 현재 코드에서는 비활성화된 자식의 Script가 따로 실행되지는 않습니다, 게다가 오브젝트가 비활성화되면 GameManager 내 event에서 빠져나오도록 코드를 작성했습니다
        // 그러나 Start에서 처음 Path를 설정하기 때문에 Object Pooling을 위해 생성한 모든 enemy 중 changePath를 발생하기 전 비활성화 된 자식들은 해당 결과에 영향을 받지 않습니다
        // 따라서 A* 내 최단경로로 이미 받은 Path값에서 비활성화된 상태에서 Path내 Node가 삭제된다고 해도 이놈은 바뀐 Path값을 반영하지 않습니다
        // 그러니 OnEnable할때도 FindPath를 하는것이 전 더 괜찮다고 생각합니다. 물론 부하는 더 커질겁니다

        // 다시 코드를 보니 GameManager에서 생성할 때 Path를 설정하도록 짜여 있습니다, 그러니 비활성화될때 GameManager 내 enemyPath도 수정하도록 하면 좋을 것 같습니다 (OnEnable할때마다 경로를 재설정하는 것보다 부하도 훨씬 적을겁니다)
    }
}
