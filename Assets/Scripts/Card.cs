using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[SelectionBase]
public class Card : MonoBehaviour
{
    [SerializeField] private CardScriptableObject cardSO;
    [SerializeField] private TMP_Text attackText, healthText, manaText, nameText, descriptionText, loreText;
    [SerializeField] private Image characterArt, bgArt;
    [SerializeField] private float moveSpeed = 5f, rotateSpeed = 540f;

    private int attack, health, mana;
    private string cardName, description, lore;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        SetupCard();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRotation = rotToMatch;
    }

    private void SetupCard()
    {
        cardName = cardSO.name;
        description = cardSO.description;
        lore = cardSO.lore;
        attack = cardSO.attack;
        health = cardSO.health;
        mana = cardSO.mana;

        nameText.text = cardName;
        descriptionText.text = description;
        loreText.text = lore;
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();

        bgArt.sprite = cardSO.bgSprite;
        characterArt.sprite = cardSO.characterSprite;
    }

}
