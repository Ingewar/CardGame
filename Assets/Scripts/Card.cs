using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[SelectionBase]
public class Card : MonoBehaviour
{
    public bool isInHand;
    public int handIndex;
    public CardScriptableObject cardSO;
    [SerializeField] private TMP_Text attackText, healthText, manaText, nameText, descriptionText, loreText;
    [SerializeField] private Image characterArt, bgArt;
    [SerializeField] private float moveSpeed = 5f, rotateSpeed = 540f;
    [SerializeField] private Vector3 hoverOffset = new Vector3(0, 1f, .5f);
    [SerializeField] private Vector3 selectedCardOffset = new Vector3(0, 2, 0);
    [SerializeField] private LayerMask desktopLayer, placementLayer;

    private int attack, health, mana;
    private string cardName, description, lore;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    private bool isSelected;
    private Collider col;
    private bool justPressed;
    private CardPlacement cardPlacement;

    private void Awake()
    {
        col = GetComponent<Collider>();
        SetupCard();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, desktopLayer))
            {
                MoveToPoint(hit.point + selectedCardOffset, Quaternion.identity);
            }

            if (Input.GetMouseButtonDown(1))
            {
                ReturnToHand();
            }

            if (Input.GetMouseButtonUp(0) && justPressed == false)
            {
                if (Physics.Raycast(ray, out hit, 100f, placementLayer))
                {
                    CardPlacement selectedPoint = hit.collider.GetComponent<CardPlacement>();

                    if (selectedPoint.activeCard is null && selectedPoint.isPlayer)
                    {
                        if (BattleController.instance.playerMana >= mana)
                        {
                            selectedPoint.activeCard = this;
                            cardPlacement = selectedPoint;
                            MoveToPoint(selectedPoint.transform.position, Quaternion.identity);
                            isInHand = false;
                            isSelected = false;

                            HandController.instance.RemoveCardFromHand(this);

                            BattleController.instance.SpendMana(mana);
                        }
                        else
                        {
                            ReturnToHand();
                            UIController.instance.ShowManaWarning();
                        }

                    }
                    else
                    {
                        ReturnToHand();
                    }
                }
                else { ReturnToHand(); }
            }

        }

        justPressed = false;
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

    private void OnMouseOver()
    {
        if (isInHand)
        {
            MoveToPoint(HandController.instance.cardPositions[handIndex] + hoverOffset, Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        if (isInHand)
        {
            MoveToPoint(HandController.instance.cardPositions[handIndex], HandController.instance.minPos.rotation);
        }
    }

    private void OnMouseDown()
    {
        if (isInHand)
        {
            isSelected = true;
            col.enabled = false;

            justPressed = true;
        }
    }

    public void ReturnToHand()
    {
        isSelected = false;
        col.enabled = true;
        MoveToPoint(HandController.instance.cardPositions[handIndex], HandController.instance.minPos.rotation);
    }
}
