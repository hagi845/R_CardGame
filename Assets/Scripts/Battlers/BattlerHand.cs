using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerHand : MonoBehaviour
{
    List<Card> list = new List<Card>();

    public bool IsEmpty => list.Count == 0;

    public void Add(Card card)
    {
        list.Add(card);
        card.transform.SetParent(transform);
    }

    public void Remove(Card card)
    {
        list.Remove(card);
    }

    public void ResetPosition()
    {
        list.Sort((card0, card1) => card0.Base.Number - card1.Base.Number);
        for (int i = 0; i < list.Count; i++)
        {
            float posX =( i-list.Count/2f) * 1.4f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }

    public Card RandomPop()
    {
        int r = Random.Range(0, list.Count);
        Card card = list[r];
        Remove(card);
        return card;
    }
}
