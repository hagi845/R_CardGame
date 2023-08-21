using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] CardBase[] cardBases;
    [SerializeField] Card cardPrefab;

    public Card Spawn(int number)
    {
      var card =  Instantiate(cardPrefab);
        card.Set(cardBases[number]);
        return card;
    }
}
