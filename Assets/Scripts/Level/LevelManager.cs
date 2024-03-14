using UnityEngine;

public class LevelManager : AbstractSingleton<LevelManager>
{
    [SerializeField] private GameObject aIPrefab;
    private GameObject levelMarker;
    private GameObject levelContainer;
    protected override void Awake()
    {
        base.Awake();

        int level = PlayerPrefs.GetInt("Level", 1);

        var levelData = SequenceManager.Instance.Levels[level - 1];

        if (levelData is LevelDefinition levelDefinition)
        {
            TerrainGenerate(levelDefinition);
            LoadLevel(levelDefinition, ref levelContainer);
            PlaceLevelMarkers(levelDefinition, ref levelMarker);
            SpawnAICharacters(levelDefinition);
        }
    }
    public void LoadLevel(LevelDefinition levelDefinition, ref GameObject levelContainerGameobject)
    {
        if (levelContainerGameobject != null)
        {
            if (Application.isPlaying)
            {
                Destroy(levelContainerGameobject);
            }
            else
            {
                DestroyImmediate(levelContainerGameobject);
            }
        }

        levelContainerGameobject = new GameObject("Level Container");

        for (int i = 0; i < levelDefinition.Spawnables.Length; i++)
        {
            LevelDefinition.SpawnableObject spawnableObject = levelDefinition.Spawnables[i];

            if (spawnableObject == null)
                continue;
            
            Vector3 position = spawnableObject.Position;
            Vector3 eulerAngles = spawnableObject.EulerAngles;

            if(spawnableObject.SpawnablePrefab != null)
            {
                GameObject spawnable = Instantiate(spawnableObject.SpawnablePrefab, position, Quaternion.Euler(eulerAngles));
                spawnable.transform.SetParent(levelContainerGameobject.transform);
            }
        }
    }
    public void TerrainGenerate(LevelDefinition levelDefination)
    {
        float width = levelDefination.LevelWidth;
        float length = levelDefination.LevelLength + levelDefination.LevelLengthBufferEnd + levelDefination.LevelLengthBufferStart;
        float height = levelDefination.LevelThickness;
        GameObject terrain = GameObject.CreatePrimitive(PrimitiveType.Cube);
        terrain.GetComponent<MeshRenderer>().material = levelDefination.TerrainMaterial;
        terrain.transform.position = new Vector3(0, 0, length / 2 - levelDefination.LevelLengthBufferStart);
        terrain.transform.localScale = new Vector3(width, height, length);
    }
    public static void PlaceLevelMarkers(LevelDefinition levelDefinition, ref GameObject levelMarkersGameObject)
    {
        if (levelMarkersGameObject != null)
        {
            if (Application.isPlaying)
            {
                Destroy(levelMarkersGameObject);
            }
            else
            {
                DestroyImmediate(levelMarkersGameObject);
            }
        }

        levelMarkersGameObject = new GameObject("Level Markers");

        GameObject start = levelDefinition.StartPrefab;
        GameObject end = levelDefinition.EndPrefab;

        if (start != null)
        {
            GameObject go = Instantiate(start, new Vector3(start.transform.position.x, start.transform.position.y, levelDefinition.LevelLengthBufferStart), Quaternion.identity);
            go.transform.SetParent(levelMarkersGameObject.transform);
        }

        if (end != null)
        {
            GameObject go = Instantiate(end, new Vector3(end.transform.position.x, end.transform.position.y, levelDefinition.LevelLength), end.transform.rotation);
            go.transform.SetParent(levelMarkersGameObject.transform);
        }
    }
    public void SpawnAICharacters(LevelDefinition levelDefinition)
    {
        for (int i = 0; i < levelDefinition.AIPositions.Length; i++)
        {
            Vector3 position = levelDefinition.AIPositions[i];
            float velocity = levelDefinition.AIVelocities[i];

            GameObject aiCharacterObject = Instantiate(aIPrefab, position, Quaternion.identity);
            AIController aiCharacter = aiCharacterObject.GetComponent<AIController>();

            if (aiCharacter != null)
            {
                aiCharacter.Initialize(position, velocity);
            }
        }
    }
}