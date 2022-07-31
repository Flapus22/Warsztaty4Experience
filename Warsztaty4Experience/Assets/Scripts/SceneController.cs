using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonMonoBehaviour<SceneController>
{
    [field: SerializeField]
    private int FirstSceneToLoad { get; set; }

    public event Action OnSceneLoaded = delegate { };

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(Instance);
    }

    protected virtual void Start()
    {
        SceneManager.LoadScene(FirstSceneToLoad);
        SceneManager.sceneLoaded += NotifiOnSceneLoaded;
    }

    public void LoadNewScene(int scene)
    {
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    private void NotifiOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        OnSceneLoaded();
    }
}
