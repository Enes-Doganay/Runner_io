using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/LevelDefinition")]
public class LevelDefinition : AbstractLevelData
{
    public float LevelLength = 100.0f;

    // The amount of extra terrain to be added before the start of the level.
    public float LevelLengthBufferStart = 5.0f;

    // The amount of extra terrain to be added after the end of the level.
    public float LevelLengthBufferEnd = 5.0f;

    // The width of the level.
    public float LevelWidth = 5.0f;

    // The thickness of the level.
    public float LevelThickness = 0.1f;

    // True means that spawnables should snap to a grid in this level.
    public bool SnapToGrid = true;

    // The size of the grid that spawnables will snap to if SnapToGrid 
    // is true.
    public float GridSize = 1.0f;

    // The material applied to the generated terrain for this level.
    public Material TerrainMaterial;

    // A prefab placed at the start point of this level.
    public GameObject StartPrefab;

    // A prefab placed at the end of this level. This prefab should 
    // contain collision logic to complete the level.
    public GameObject EndPrefab;

    // The world positions of AI.
    public Vector3[] AIPositions;
    
    // Velocities of AI
    public float[] AIVelocities;

    // An array of all SpawnableObjects that exist in this level.
    public SpawnableObject[] Spawnables;

    [System.Serializable]
    public class SpawnableObject
    {
        // The prefab spawned by this SpawnableObject.
        public GameObject SpawnablePrefab;

        // The world position of this SpawnableObject.
        public Vector3 Position = Vector3.zero;
        
        // The rotational euler angles of this SpawnableObject.
        public Vector3 EulerAngles = Vector3.zero;

        // The world scale of this SpawnableObject.
        public Vector3 Scale = Vector3.one;

        // The base color to be applied to the materials on 
        // this SpawnableObject.
        public Color BaseColor = Color.white;
    }
}
