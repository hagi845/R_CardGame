using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject submitButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameUI gameUI;

    RuleBook ruleBook;

    private void Awake()
    {
        ruleBook = GetComponent<RuleBook>();
    }
    private void Start()
    {
        Setup();
    }

    // カードを生成して配る
    void Setup()
    {
        gameUI.Init();
        nextButton.SetActive(false);
        player.Life = 4;
        enemy.Life = 4;
        player.OnSubmitAction = SubmitedAction;
        enemy.OnSubmitAction = SubmitedAction;
        SendCardsTo(player,false);
        SendCardsTo(enemy,true);
    }

    void SubmitedAction()
    {
        if(player.IsSubmitted && enemy.IsSubmitted)
        {
            submitButton.SetActive(false);
            StartCoroutine(CardsBattle());
        }
        else if (player.IsSubmitted)
        {
            submitButton.SetActive(false);

            enemy.RandomSubmit();
        }
        else if (enemy.IsSubmitted)
        {
            // playerの提出を待つ
        }
    }

    void SendCardsTo(Battler battler, bool isEnemy)
    {
        for (int i = 0; i < 8; i++)
        {
            var card = cardGenerator.Spawn(i,isEnemy);
            battler.SetCardToHand(card);
        }
        battler.Hand.ResetPosition();
    }

    Result _gameResult;

    IEnumerator CardsBattle()
    {
        yield return new WaitForSeconds(1f);
        enemy.SubmitCard.Open();
        yield return new WaitForSeconds(0.7f);
        _gameResult = ruleBook.GetResult(player, enemy);

        switch (_gameResult)
        {
            case Result.TurnWin:
            case Result.GameWin:
                gameUI.ShowTurnResult("WIN");
                enemy.Life--;
                break;
            case Result.TurnWin2:
                gameUI.ShowTurnResult("WIN");
                enemy.Life -= 2;
                break;
            case Result.TurnLose:
            case Result.GameLose:
                gameUI.ShowTurnResult("LOSE");
                player.Life--;
                break;
            case Result.TurnLose2:
                gameUI.ShowTurnResult("LOSE");
                player.Life -= 2;
                break;
            case Result.TurnDraw:
                gameUI.ShowTurnResult("DRAW");
                break;
            default:
                break;
        }

        nextButton.SetActive(true);
        Debug.Log("nextButton: true");
        
        gameUI.Showlifes(player.Life, enemy.Life);
    }

    public void OnNextButton()
    {
        if (_gameResult == Result.GameWin || _gameResult == Result.GameLose || player.Life <= 0 || enemy.Life <= 0)
        {
            ShowResult(_gameResult);
        }
        else
        {
            SetupNextTurn();
        }
    }

    void ShowResult(Result result)
    {
        if(result == Result.GameWin)
        {
            gameUI.ShowGameResult("WIN");
        }
        else if (result == Result.GameLose)
        {
            gameUI.ShowGameResult("WIN");
        }
        else if (player.Life <= 0 || enemy.Life <= 0)
        {
            gameUI.ShowGameResult("LOSE");
        }
        else if(enemy.Life <= 0)
        {
            gameUI.ShowGameResult("WIN");
        }
    }

    void SetupNextTurn()
    {
        player.SetupNextTurn();
        enemy.SetupNextTurn();
        gameUI.SetupNextTurn();
        submitButton.SetActive(true);
        nextButton.SetActive(false);
        Debug.Log("nextButton: false");

        if (enemy.IsFirstSubmit)
        {
            enemy.RandomSubmit();
            enemy.IsFirstSubmit = false;    
        }
        if (player.IsFirstSubmit)
        {
            // TODO: playerが先に出す
        }
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnTitleButton()
    {
        SceneManager.LoadScene("Title");
    }

}
