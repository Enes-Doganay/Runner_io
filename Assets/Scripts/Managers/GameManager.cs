using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AbstractSingleton<GameManager>
{
    public static event Action GameStarted;
    public bool IsGameStarted { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void StartGame()
    {
        GameStarted?.Invoke();
        IsGameStarted = true;
    }
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        GameObject[] levelManagers = SequenceManager.Instance.LevelManagers;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        foreach (var prefab in levelManagers)
        {
            Instantiate(prefab);
        }

    }
    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        PlayerPrefs.SetInt("Level", currentLevel + 1);
        LoadSceneAsync("Level");
        IsGameStarted = false;
    }
    public void RestartLevel()
    {
        LoadSceneAsync("Level");
        IsGameStarted = false;
    }
}