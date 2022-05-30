using Assets.Scripts.Logic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scorePerSecondText;


    void Start()
    {
        StartCoroutine(IdleFarm());
    }

    void Update()
    {
        scoreText.text = PlayerStats.TotalScore.ToString("N2");
        scorePerSecondText.text = PlayerStats.ScoresPerSecond.ToString("N2") + "/sec";
    }

    public void onClickObjectClick()
    {
        PlayerStats.TotalScore += PlayerStats.ClickCost;
        
    }
    IEnumerator IdleFarm()
    {
        yield return new WaitForSeconds(1);

        PlayerStats.TotalScore += PlayerStats.ScoresPerSecond;
        
        StartCoroutine(IdleFarm());
    }
}
