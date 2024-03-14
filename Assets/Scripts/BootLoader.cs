using UnityEngine;

public class BootLoader : MonoBehaviour
{
    [SerializeField]
    private SequenceManager sequenceManagerPrefab;

    private void Start()
    {
        Instantiate(sequenceManagerPrefab);
        SequenceManager.Instance.Initialize();
    }
}