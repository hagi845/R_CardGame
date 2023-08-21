using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text numberText;
    [SerializeField] Image icon;
    [SerializeField] Text descriptionText;

    // カードUI
    // カード内の処理
    public void Set(CardBase cardBase)
    {
        nameText.text = cardBase.Name;
        numberText.text= cardBase.Number.ToString();
        icon.sprite = cardBase.Icon;
        descriptionText.text = cardBase.Description;

    }
}
