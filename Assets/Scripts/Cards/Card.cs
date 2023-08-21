using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriptionText;

    public CardBase Base { get; private set; }

    public UnityAction<Card> OnClickCard;

    // カードUI
    // カード内の処理
    public void Set(CardBase cardBase)
    {
        Base = cardBase;
        nameText.text = cardBase.Name;
        numberText.text= cardBase.Number.ToString();
        icon.sprite = cardBase.Icon;
        descriptionText.text = cardBase.Description;

    }

    public void OnClick()
    {
        OnClickCard?.Invoke(this);
    }
}
