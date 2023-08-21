using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject submitButton;
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
        player.Life = 4;
        enemy.Life = 4;
        player.OnSubmitAction = SubmitedAction;
        enemy.OnSubmitAction = SubmitedAction;
        SendCardsTo(player);
        SendCardsTo(enemy);
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

    void SendCardsTo(Battler battler)
    {
        for (int i = 0; i < 8; i++)
        {
            var card = cardGenerator.Spawn(i);
            battler.SetCardToHand(card);
        }
        battler.Hand.ResetPosition();
    }

    IEnumerator CardsBattle()
    {
        yield return new WaitForSeconds(1f);
        var result = ruleBook.GetResult(player, enemy);

        switch (result)
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
                gameUI.ShowTurnResult("WIN");
                player.Life -= 2;
                break;
            case Result.TurnDraw:
                gameUI.ShowTurnResult("DRAW");
                break;
            default:
                break;
        }

        gameUI.Showlifes(player.Life, enemy.Life);
        yield return new WaitForSeconds(1f);

        if (player.Life <= 0 || enemy.Life <= 0 )
        {
            ShowResult(result);
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
    }
}
