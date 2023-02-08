using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instanceBM; // 싱글톤용
    public Bullet[] bulletCode = new Bullet[100];
    public Sword[] swordCode = new Sword[100];
    // Start is called before the first frame update
    void Awake()
    {
        if (instanceBM != this)
        {
            instanceBM = this;
        }
    }

    public void BulletLinearShoot(Vector3 startPos,Vector3 dir, float speed)
    {
        GameObject bullet = ObjectManager.instance.MakeObj(ObjectManager.Obj.playerBullet);
        int bulletNum = getLastBulletNum();
        bullet.transform.position = startPos;
        bulletCode[bulletNum].dir = dir;
        bulletCode[bulletNum].speed = speed;
        bulletCode[bulletNum].angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90.0f;

    }

    public GameObject SwordMake(Transform trs,Vector3 rot,float range, float speed)
    {
        Debug.Log(trs+" "+rot+" "+range+" "+speed);
        GameObject sword = ObjectManager.instance.MakeObj(ObjectManager.Obj.sword);
        
        sword.transform.parent = trs;
        sword.transform.position = trs.position+new Vector3(0,range,0);
        sword.transform.Rotate(rot);
        Debug.Log("현재 칼" + getLastSwordNum());
        int swordNum = getLastSwordNum();
        
        swordCode[swordNum].speed = speed;
        swordCode[swordNum].startPos = trs.position;


        return sword;
    }

    public int getLastBulletNum()
    {
        for (int i = 0; i < bulletCode.Length; i++)
        {
            if (!bulletCode[i].gameObject.activeSelf)
            {
                return i - 1;
            }
        }
        return -1;
    }

    public int getLastSwordNum()
    {
        for (int i = 0; i < swordCode.Length; i++)
        {
            if (!swordCode[i].gameObject.activeSelf)
            {
                return i - 1;
            }
        }
        return -1;
    }

}
