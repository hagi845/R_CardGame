using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBase : ScriptableObject
{
    // カードの基礎データ
    [SerializeField] new  string name;
    [SerializeField] CardType type;
    [SerializeField] int number;
    [SerializeField] Sprite icon;
    [TextArea]
    [SerializeField] string description;

    public string Name { get => name;  }
    public CardType Type { get => type; }
    public int Number { get => number; }
    public Sprite Icon { get => icon; }
    public string Description { get => description; }
}

public enum CardType
{
    Clown,
    Princess,
    Spy,
    Assassin,
    Minister,
    Magician,
    Shogun,
    Prince,
}
