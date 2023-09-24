using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card", fileName = "New Card")]
public class CardScriptableObject : ScriptableObject
{
    public string name;

    [TextArea]
    public string description, lore;

    public int attack, health, mana;

    public Sprite characterSprite, bgSprite;
}
