using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [field: SerializeField]
    private TextMeshProUGUI MoneyText { get; set; }

    [field: Header("Tower Info")]
    [field: SerializeField]
    public TextMeshProUGUI DmgText { get; set; }
    [field: SerializeField]
    public TextMeshProUGUI FireRateText { get; set; }


    protected virtual void OnEnable()
    {
        if (SceneController.Instance != null)
            SceneController.Instance.OnSceneLoaded += AtachEvents;
    }

    protected virtual void OnDisable()
    {
        DetachEvents();
    }

    private void AtachEvents()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PropertyChanged += MoneyUpdate;
            TowerManager.Instance.PropertyChanged += TowerInfoUpdate;
        }
    }

    private void DetachEvents()
    {

        if (SceneController.Instance != null)
            SceneController.Instance.OnSceneLoaded -= AtachEvents;
    }

    public void TakeTowerToSpawn(GameObject TowerPrefab)
    {
        if (TowerPrefab != null)
        {
            TowerManager.Instance.ChooseTowerToSpawn(TowerPrefab);
        }
    }

    public void RemoveTowerToSpawn()
    {
        TowerManager.Instance.RemoveTower();
    }

    private void MoneyUpdate(object sender, PropertyChangedEventArgs eventArgs)
    {
        MoneyText.text = sender.ToString();
    }

    private void TowerInfoUpdate(object sender, PropertyChangedEventArgs eventArgs)
    {
        if (sender != null)
        {
            DmgText.text = (sender as TowerController).TowerAttackData.Damage.ToString();
            FireRateText.text = (sender as TowerController).TowerAttackData.FireRate.ToString();
        }
        else
        {
            DmgText.text = "0";
            FireRateText.text = "0";
        }
    }
}
