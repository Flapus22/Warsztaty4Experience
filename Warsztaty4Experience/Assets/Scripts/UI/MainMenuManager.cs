using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : SingletonMonoBehaviour<MainMenuManager>
{
    [field: SerializeField]
    private GameObject CurrentWindow { get; set; }

    public void ShowChosenWindow(GameObject showWindow)
    {
        CurrentWindow?.SetActive(false);
        showWindow?.SetActive(true);
        CurrentWindow = showWindow;
    }

    public void LoadLevel(int level)
    {
        SceneController.Instance.LoadNewScene(level);
        UIManager.Instance.LoadGame();
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
