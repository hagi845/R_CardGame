using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField] BattlerHand hand;

    public BattlerHand Hand { get => hand;}

    public void SetCardToHand(Card card)
    {
        hand.Add(card);
        card.OnClickCard = SelectedCard;
    }

    void SelectedCard(Card card)
    {
        //Debug.Log(card.Base.Number);
    }
}
