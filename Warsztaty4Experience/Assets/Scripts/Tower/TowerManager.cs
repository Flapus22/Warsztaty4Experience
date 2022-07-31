using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TowerManager : SingletonMonoBehaviour<TowerManager>, INotifyPropertyChanged
{

    private TowerController chosenTower;
    public TowerController ChosenTower
    {
        get => chosenTower;
        private set
        {
            chosenTower = value;
            OnPropertyChanged(value);
        }
    }

    private List<TowerController> TowerList { get; set; } = new List<TowerController>();

    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    protected virtual void Update()
    {
        //if (Input.GetKey(KeyCode.Space)) SpawnTower();
        if (Input.GetKey(KeyCode.Mouse0)) PlaceTowerOnPosition();
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
            Debug.Log("TowerManager cannot AttachEvent");
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

    public void ChooseTowerToSpawn(GameObject towerPrefab)
    {
        Destroy(ChosenTower?.gameObject);
        ChosenTower = Instantiate(towerPrefab).GetComponent<TowerController>();
    }

    private void Initialize()
    { }

    private void PlaceTowerOnPosition()
    {
        if (ChosenTower?.CheckIfCanBePlaced() == true && GameManager.Instance.TryBuyTower(ChosenTower))
        {
            TowerList.Add(ChosenTower);
            ChosenTower.PlaceTower();
            ChosenTower = null;
        }
    }

    private void DestroyAllTower()
    {
        for (int i = 0; i < TowerList.Count; i++)
            Destroy(TowerList[i]);
        TowerList.Clear();
    }

    public void RemoveTower()
    {
        Destroy(ChosenTower.gameObject);
        ChosenTower = null;
    }

    private void OnPropertyChanged(object value, [CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(value, new PropertyChangedEventArgs(propertyName));
    }

    private void HandleGameStart()
    {

    }

    private void HandleGameEnd()
    {
        DestroyAllTower();
    }
}
