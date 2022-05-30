using Assets.Scripts.Logic;
using System;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveProgress();
        }
    }
#else
    private void OnApplicationQuit()
    {
        SaveProgress();
    }
#endif
    void SaveProgress()
    {
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        PlayerPrefs.SetFloat("TotalScore", PlayerStats.TotalScore);
        PlayerPrefs.SetFloat("ClickCost", PlayerStats.ClickCost);
        PlayerPrefs.SetFloat("TotalScorePerSecond", PlayerStats.ScoresPerSecond);
    }

}
