using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text turnResultText;
    [SerializeField] Text playerLifeText;
    [SerializeField] Text enemyLifeText;
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;
    [SerializeField] GameObject playerAddNumberObj;
    [SerializeField] GameObject enemyAddNumberObj;

    public void Init()
    {
        turnResultText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
    }

    public void UpdateAddNumber(bool isPlayerAddNumber,bool isEnemyAddNumber)
    {
        playerAddNumberObj.SetActive(isPlayerAddNumber);
        enemyAddNumberObj.SetActive(isEnemyAddNumber);
    }

    public void Showlifes(int playerLife, int enemyLife)
    {
        playerLifeText.text = $"x{playerLife.ToString()}";
        enemyLifeText.text = $"x{enemyLife.ToString()}";
    }

    public void ShowTurnResult(string result)
    {
        turnResultText.gameObject.SetActive(true);
        turnResultText.text = result;
    }

    public void ShowGameResult(string result)
    {
        resultPanel.SetActive(true);
        resultText.text = result;
    }

    public void SetupNextTurn()
    {
        turnResultText.gameObject.SetActive(false);
    }
}
