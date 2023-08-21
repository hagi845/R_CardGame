using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;
    [SerializeField] GameObject submitButton;

    private void Start()
    {
        Setup();
    }

    // カードを生成して配る
    void Setup()
    {
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
            // 勝利判定
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
}
