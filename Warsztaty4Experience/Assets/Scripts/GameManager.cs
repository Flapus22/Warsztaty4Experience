using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : SingletonMonoBehaviour<GameManager>, IHitable, INotifyPropertyChanged
{
    [field: SerializeField]
    public float Point { get; set; }
    [field: SerializeField]
    private int MaxHealth { get; set; }
    [field: SerializeField]
    private int Health { get; set; }

    [field: Space]
    [field: SerializeField]
    private int MaxMoney { get; set; }
    [field: SerializeField]
    private int MoneyOnStart { get; set; }

    public int Money
    {
        get => money;
        set
        {
            if (value != money)
            {
                money = value;
                OnPropertyChanged(value);
            }
        }
    }
    [SerializeField]
    private int money;
    //[field: SerializeField]
    //public int Money { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public event Action OnGameStar = delegate { };
    public event Action OnGameEnd = delegate { };

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    protected virtual void Start()
    {
        NotifyOnGameStart();
    }

    private void OnPropertyChanged(object value, [CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(value, new PropertyChangedEventArgs(propertyName));
    }

    private void Initialize()
    {
        Health = MaxHealth;
        Money = MoneyOnStart;
    }

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        if (Health <= 0)
        {
            //GameOver
            NotifyOnGameEnd();
        }
    }

    public bool TryBuyTower(TowerController tower)
    {
        if (Money >= tower?.TowerPrice)
        {
            Money -= tower.TowerPrice;
            return true;
        }
        return false;
    }

    public void AddCoins(int amount)
    {
        Money += amount;
        if (Money > MaxMoney)
        {
            Money = MaxMoney;
        }
    }

    public void AddPoint()
    {
        Point++;
    }

    private void NotifyOnGameStart()
    {
        OnGameStar();
    }

    private void NotifyOnGameEnd()
    {
        OnGameEnd();
    }
}
