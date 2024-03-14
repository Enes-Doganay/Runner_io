using System.Collections.Generic;
using UnityEngine;

public class PlayerRanking : AbstractSingleton<PlayerRanking>
{
    private List<Controller> runners; // List of all runners
    private int playerRank;
    public int PlayerRank => playerRank;

    void Start()
    {
        // Get all the runners in the scene
        runners = new List<Controller>(FindObjectsOfType<Controller>());
    }

    void Update()
    {
        runners.Sort((x, y) => y.transform.position.z.CompareTo(x.transform.position.z)); // Sort runners by z position

        playerRank = runners.IndexOf(GetComponent<Controller>()) + 1; // Get player rank

        UIManager.Instance.SetRankUI(playerRank); // Send player rank to UIManager
    }
    public int CalculateRankReward()
    {
        switch(playerRank)
        {
            case 1:
                return 150;
            case 2:
                return 100;
            case 3:
                return 50;
            case 4: 
                return 20;
            case 5: 
                return 10;
        }
        return 0;
    }
}