using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : SingletonMonoBehaviour<UIManager>
{
   
    //private int MoneyText { get; set; } = 1;

    [field: SerializeField]
    private GameObject MainMenu { get; set; }
    [field: SerializeField]
    private GameObject GameUI { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadGame()
    {
        GameUI.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        MainMenu.SetActive(true);
        GameUI.SetActive(false);
    }
}
