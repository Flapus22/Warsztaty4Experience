using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : SingletonMonoBehaviour<BulletManager>
{
    [field: SerializeField]
    private List<Bullet> BulletList { get; set; }
    private int NextBulletIndex { get; set; }

    public void ShootToEnemyBullet(Vector3 startPosition, Enemy target, int damage)
    {
        BulletList[NextBulletIndex].SetBulletData(startPosition, target, damage);
        NextBulletIndex++;
        NextBulletIndex = NextBulletIndex % BulletList.Count;
    }
}
