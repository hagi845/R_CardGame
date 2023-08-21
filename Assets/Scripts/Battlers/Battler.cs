using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;
    [SerializeField] SubmitPosition submitPosition;

    public bool IsSubmitted { get; private set; }

    public UnityAction OnSubmitAction;

    public BattlerHand Hand => hand;
    public Card SubmitCard => submitPosition.SubmitCard;

    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        if (IsSubmitted) return;

        if (submitPosition.SubmitCard)
        {
            hand.Add(submitPosition.SubmitCard);
        }
        hand.Remove(card);
        submitPosition.Set(card);
        hand.ResetPosition();
    }

    public void OnSubmitButton()
    {
        if (!submitPosition.SubmitCard) return;
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
    }

    public void RandomSubmit()
    {
        Card card = hand.RandomPop();
        submitPosition.Set(card);
        IsSubmitted = true;
        OnSubmitAction?.Invoke();
        hand.ResetPosition();
    }

    public void SetupNextTurn()
    {
        IsSubmitted = false;
        submitPosition.DeleteCard();
    }
}
