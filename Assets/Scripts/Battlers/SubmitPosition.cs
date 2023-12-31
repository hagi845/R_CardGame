using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmitPosition : MonoBehaviour
{
    // 選択されたカードを管理する
    Card submitCard;

    public Card SubmitCard { get => submitCard;}

    public void Set(Card card)
    {
        submitCard = card;
        card.transform.SetParent(transform);
        //card.transform.position = transform.position;
        card.transform.DOMove(transform.position, 0.1f);
    }

    public void DeleteCard()
    {
        Destroy(submitCard.gameObject);
        submitCard = null;
    }
}
