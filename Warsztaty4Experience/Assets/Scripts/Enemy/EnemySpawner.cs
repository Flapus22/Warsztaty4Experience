using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> EnemieList { get; set; } = new List<Enemy>();

    [field: SerializeField]
    private GameObject EnemyPrefab { get; set; }

    [field: Space]
    [field: SerializeField]
    private float SpawnDelay { get; set; }
    [field: SerializeField]
    private float MaxSpawnDelay { get; set; }
    [field: SerializeField]
    private float MinSpawnDelay { get; set; }
    [field: SerializeField]
    [field: Range(0.0001f, 2)]
    private float SpeedRateIncreaseSpawnEnemy { get; set; }

    [field: Space]
    [field: SerializeField]
    private Vector3 SpawnPosition { get; set; }
    [field: SerializeField]
    private Quaternion SpawnRotation { get; set; }
    [field: SerializeField]
    private Transform Target { get; set; }

    private float TimeToSpawn { get; set; }
    private bool CanSpawnEnemy { get; set; }

    protected virtual void Awake()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        SpawnEnemyOnKeyPressed();
        RegularSpawn();
        if (Input.GetKeyUp(KeyCode.P))
            ToggleSpawnEnemy();
        if (Input.GetKeyUp(KeyCode.Z))
            DestroyAllEnemy();
    }

    protected virtual void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded += AtachEvents;
    }

    protected virtual void OnDisable()
    {
        DetachEvents();
    }

    public void AtachEvents()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStar += HandleGameStart;
            GameManager.Instance.OnGameEnd += HandleGameEnd;
        }
        else
        {
            Debug.Log("EnemySpawner cannot AttachEvent");
        }
    }

    private void DetachEvents()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStar -= HandleGameStart;
            GameManager.Instance.OnGameEnd -= HandleGameEnd;
        }

        if (SceneController.Instance != null)
        {
            SceneController.Instance.OnSceneLoaded -= AtachEvents;
        }
    }

    private void Initialize()
    {
        SpawnDelay = MaxSpawnDelay;
    }

    private void ToggleSpawnEnemy()
    {
        CanSpawnEnemy = !CanSpawnEnemy;
    }

    private void SpawnEnemyOnKeyPressed()
    {
        if (Input.GetKeyUp(KeyCode.L))
            SpawnEnemy();
    }

    private void DestroyAllEnemy()
    {
        for (int i = 0; i < EnemieList.Count; i++)
            Destroy(EnemieList[i]);
        EnemieList.Clear();
    }

    private void RegularSpawn()
    {
        if (CanSpawnEnemy)
        {
            TimeToSpawn += Time.deltaTime;
            if (TimeToSpawn >= SpawnDelay)
            {
                TimeToSpawn = 0;
                SpawnEnemy();
            }
            ChangeSpawnDelay();
        }
    }

    private void ChangeSpawnDelay()
    {
        SpawnDelay = Mathf.Lerp(MinSpawnDelay, MaxSpawnDelay, GameManager.Instance.Point * SpeedRateIncreaseSpawnEnemy);
    }

    private void SpawnEnemy()
    {
        Enemy spawnedEnemy = Instantiate(EnemyPrefab, SpawnPosition, SpawnRotation, null).GetComponent<Enemy>();
        spawnedEnemy.SetDestination(Target.position);
        spawnedEnemy.OnEnemyDestroy.AddListener(UnregisterEnemy);
        EnemieList.Add(spawnedEnemy);
    }

    private void UnregisterEnemy(Enemy enemy)
    {
        EnemieList.Remove(enemy);
        enemy.OnEnemyDestroy.RemoveListener(UnregisterEnemy);
    }

    private void HandleGameStart()
    {
        ToggleSpawnEnemy();
    }

    private void HandleGameEnd()
    {
        ToggleSpawnEnemy();
        DestroyAllEnemy();
    }
}
