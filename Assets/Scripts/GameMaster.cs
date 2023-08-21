using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] Battler player;
    [SerializeField] Battler enemy;
    [SerializeField] CardGenerator cardGenerator;

    private void Start()
    {
        Setup();
    }

    // カードを生成して配る
    void Setup()
    {
        SendCardsTo(player);
        SendCardsTo(enemy);
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
