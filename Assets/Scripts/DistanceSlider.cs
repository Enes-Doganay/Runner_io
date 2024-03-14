using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSlider : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelText;
    
    private Slider distanceSlider;
    private void Start()
    {
        distanceSlider = GetComponent<Slider>();
        
        Initialize();
    }
    private void Initialize()
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        levelText.text = level.ToString();

        if (SequenceManager.Instance != null)
        {
            var levelData = SequenceManager.Instance.Levels[level - 1];
            if (levelData is LevelDefinition levelDefinition)
            {
                distanceSlider.maxValue = levelDefinition.LevelLength - levelDefinition.LevelLengthBufferEnd - levelDefinition.LevelLengthBufferStart;
                distanceSlider.minValue = 0;
            }
        }
    }
    private void Update()
    {
        if (distanceSlider.gameObject.activeInHierarchy && PlayerController.Instance != null)
        {
            distanceSlider.value += PlayerController.Instance.MoveSpeed * Time.deltaTime;
        }
    }
}
