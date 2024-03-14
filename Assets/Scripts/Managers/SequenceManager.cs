using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceManager : AbstractSingleton<SequenceManager>
{
    [SerializeField]
    private GameObject[] preloadAssets;

    [SerializeField]
    private AbstractLevelData[] levels;

    [SerializeField]
    private GameObject[] levelManagers;
    public AbstractLevelData[] Levels => levels;
    public GameObject[] LevelManagers => levelManagers;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        GameManager.Instance.LoadSceneAsync("Level");
    }
    public void Initialize()
    {
        InstantiatePreloadAssets();
    }
    private void InstantiatePreloadAssets()
    {
        foreach(var asset in preloadAssets)
        {
            Instantiate(asset);
        }
    }
}