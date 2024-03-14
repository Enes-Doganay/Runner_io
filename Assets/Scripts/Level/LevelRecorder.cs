using UnityEditor;
using UnityEngine;

public class LevelRecorder : MonoBehaviour
{
    [SerializeField]
    LevelDefinition levelDefinition;

    [Header("Terrain")]
    public float LevelLength = 100.0f;
    public float LevelLengthBufferStart = 5.0f;
    public float LevelLengthBufferEnd = 5.0f;
    public float LevelWidth = 5.0f;
    public float LevelThickness = 0.1f;
    public bool SnapToGrid = true;
    public float GridSize = 1.0f;
    public Material TerrainMaterial;

    [Header("Prefabs")]
    public GameObject StartPrefab;
    public GameObject EndPrefab;

    private GameObject terrain;
    [SerializeField]
    private GameObject startPrefab;
    [SerializeField]
    private GameObject endPrefab;
    void OnValidate()
    {
        // Deðerler deðiþtirildiðinde terenin ölçeðini güncelle
        terrain = GameObject.Find("Terrain");

        UpdateTerrain();
    }

    public void UpdateTerrain()
    {
        // Yeni ölçek deðerlerini belirle
        float newLength = LevelLength;
        float newWidth = LevelWidth;
        float newHeight = LevelThickness;

        // GameObject'in ölçek deðerlerini güncelle
        if(terrain != null) 
        terrain.transform.localScale = new Vector3(newWidth, newHeight, newLength + LevelLengthBufferEnd + LevelLengthBufferStart);
        terrain.transform.position = new Vector3(0, 0, newLength / 2);
        TerrainMaterial.mainTextureScale = new Vector2(1, -newLength / 2);

        if (endPrefab != null)
        {
            endPrefab.transform.position = new Vector3(0, LevelThickness, LevelLength);
        }

        if (startPrefab != null)
        {
            startPrefab.transform.position = new Vector3(0, LevelThickness, LevelLengthBufferStart);
            startPrefab.transform.localScale = new Vector3(LevelWidth, LevelThickness, 1);
        }
    }
    public void SaveLevel()
    {
        Spawnable[] spawnables = (Spawnable[])Object.FindObjectsOfType(typeof(Spawnable));
        levelDefinition.Spawnables = new LevelDefinition.SpawnableObject[spawnables.Length];
        for (int i = 0; i < spawnables.Length; i++)
        {
            try
            {
                levelDefinition.Spawnables[i] = new LevelDefinition.SpawnableObject()
                {
                    SpawnablePrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(spawnables[i].gameObject),
                    Position = spawnables[i].transform.position,
                    EulerAngles = spawnables[i].transform.eulerAngles,
                    Scale = spawnables[i].transform.lossyScale,
                };
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        // Overwrite source level definition with current version

        levelDefinition.LevelLength = LevelLength;
        levelDefinition.LevelLengthBufferStart = LevelLengthBufferStart;
        levelDefinition.LevelLengthBufferEnd = LevelLengthBufferEnd;
        levelDefinition.LevelWidth = LevelWidth;
        levelDefinition.LevelThickness = LevelThickness;
        levelDefinition.TerrainMaterial = TerrainMaterial;
        levelDefinition.StartPrefab = StartPrefab;
        levelDefinition.EndPrefab = EndPrefab;
        // Overwrite source level definition with current version
        EditorUtility.SetDirty(levelDefinition);
        //levelDefinition.SaveValues(levelDefinition);
    }
}
