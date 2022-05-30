using Assets.Scripts.Logic;
using System;
using UnityEngine;

public class GameLoadManager : MonoBehaviour
{

    void Start()
    {
        if (!PlayerPrefs.HasKey("IsFirstRun"))
        {
            PlayerPrefs.SetInt("IsFirstRun", 1);
        }  

        //если первый запуск
        if (PlayerPrefs.GetInt("IsFirstRun") == 1)
        {
            PlayerStats.ClickCost = 1;
            PlayerStats.TotalScore = 0;
            PlayerStats.ScoresPerSecond = 0;
            IBuildingService bs = new BuildingService();
            bs.GenerateDefaultBuildings();

            PlayerPrefs.SetInt("IsFirstRun", 0);
        }
        else if (PlayerPrefs.GetInt("IsFirstRun") == 0)
        {
            PlayerStats.TotalScore = PlayerPrefs.GetFloat("TotalScore");
            PlayerStats.ClickCost = PlayerPrefs.GetFloat("ClickCost");
            PlayerStats.ScoresPerSecond = PlayerPrefs.GetFloat("TotalScorePerSecond");
            AddOfflineScores();
        }
    }
    private void AddOfflineScores()
    {
        TimeSpan ts;
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            PlayerStats.TotalScore += (float)ts.TotalSeconds * PlayerStats.ScoresPerSecond;
        }
    }
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteKey("IsFirstRun");
    }
}


