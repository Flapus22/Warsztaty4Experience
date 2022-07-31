using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [field: SerializeField]
    private float Speed { get; set; }
    [field: SerializeField]
    private float DelayToResetPosition { get; set; }
    private float CurrentDelay { get; set; }

    private Enemy TargetEnemy { get; set; }
    private int Damage { get; set; }


    protected virtual void Update()
    {
        if (TargetEnemy != null)
        {
            CurrentDelay += Time.deltaTime;
            if (CurrentDelay > DelayToResetPosition)
            {
                StopTrackEnemy();
                CurrentDelay = 0;
            }

            Vector3 dir = TargetEnemy.transform.position - transform.position;
            float distanceThisFrame = Speed * Time.deltaTime;

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(TargetEnemy.transform);
        }
        else
        {
            StopTrackEnemy();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        TargetEnemy.TakeDamage(Damage);
        StopTrackEnemy();
    }

    public void SetBulletData(Vector3 startPosition,Enemy target, int damage)
    {
        TargetEnemy = target;
        Damage = damage;
        transform.position = startPosition;
    }

    public void StopTrackEnemy()
    {
        TargetEnemy = null;
        transform.position = BulletManager.Instance.transform.position;
    }
}
