using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{
    [field: SerializeField]
    public OnEnemyDestroyEvent OnEnemyDestroy { get; private set; }
    [field: SerializeField]
    private NavMeshAgent Agent { get; set; }
    [field: SerializeField]
    private Vector3 DestinationPosition { get; set; } // na testy
    [field: SerializeField]
    private EnemyData EnemyData { get; set; }

    private int CurrentHealth { get; set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Start()
    {
        //SetDestination(DestinationPosition);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        ReachTarget();
        DestroyEnemy();
    }

    protected virtual void OnDestroy()
    {
        OnEnemyDestroy.Invoke(this);
    }

    private void Initialize()
    {
        Agent.speed = EnemyData.Speed;
        CurrentHealth = EnemyData.Health;
    }

    public void SetDestination(Vector3 target)
    {
        Agent.SetDestination(target);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            GameManager.Instance.AddCoins(EnemyData.CoinsDropped);
            GameManager.Instance.AddPoint();
            DestroyEnemy();
        }
    }

    private void ReachTarget()
    {
        GameManager.Instance.TakeDamage(EnemyData.Attack);
    }
}
