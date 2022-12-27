using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instanceBM; // 싱글톤용
    public Bullet[] bulletCode = new Bullet[100];
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

}
