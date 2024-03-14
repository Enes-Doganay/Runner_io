using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : AbstractSingleton<UIManager>
{
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI rankText;
    
    [SerializeField] private GameObject abilityPanel;
    [SerializeField] private TextMeshProUGUI abilityText;
    [SerializeField] private TextMeshProUGUI threatDetectedText;


    [Header("Victory Panel")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button getButton;
    [SerializeField] private Button noThanksButton;
    [SerializeField] private TextMeshProUGUI victoryRankText;
    [SerializeField] private TextMeshProUGUI victoryRewardText;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;

    [SerializeField] private Q_Vignette_Single fullScreenEffect;
    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
    }

    public void CollectedAbility(Ability ability)
    {
        abilityText.text = ability.Name;
        abilityPanel.SetActive(true);
    }
    public void RemoveAbility()
    {
        abilityPanel.SetActive(false);
    }
    public void ThreatDetected()
    {
        threatDetectedText.gameObject.SetActive(true);
        fullScreenEffect.StartFadeIn();
    }
    public void ThreatCleared()
    {
        threatDetectedText.gameObject.SetActive(false);
        fullScreenEffect.FadeOut();
    }
    public void SetVictoryPanel()
    {
        //AdsManager.Instance.LoadRewardedAd();

        victoryPanel.SetActive(true);

        int reward = PlayerRanking.Instance.CalculateRankReward();

        victoryRankText.text = PlayerRanking.Instance.PlayerRank.ToString();
        victoryRewardText.text = reward.ToString();

        getButton.onClick.RemoveAllListeners();
        getButton.onClick.AddListener(() =>
        {
            AdsManager.Instance.ShowRewardedAd(reward);
            AdsManager.Instance.RewardedAd.OnAdFullScreenContentClosed += () =>
            {
                GameManager.Instance.NextLevel();
            };

        });

        noThanksButton.onClick.RemoveAllListeners();
        noThanksButton.onClick.AddListener(() => GameManager.Instance.NextLevel());
    }
    public void SetGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
    }
    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SetRankUI(int rank)
    {
        rankText.text = rank + "/5";
    }
}