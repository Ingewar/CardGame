using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static HandController instance;

    void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<Card> cards = new List<Card>();
    public Transform minPos, maxPos;
    public List<Vector3> cardPositions = new List<Vector3>();

    void Start()
    {
        SetCardPositions();
    }

    void Update()
    {

    }

    public void SetCardPositions()
    {
        cardPositions.Clear();

        Vector3 pointsDistance = Vector3.zero;
        if (cards.Count > 1)
        {
            pointsDistance = (maxPos.position - minPos.position) / (cards.Count - 1);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cardPositions.Add(minPos.position + pointsDistance * i);
            cards[i].MoveToPoint(cardPositions[i], minPos.rotation);

            cards[i].isInHand = true;
            cards[i].handIndex = i;
        }

    }

    public void RemoveCardFromHand(Card card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
        }
        else
        {
            Debug.Log("Card not in hand /n" + card.name);
        }
        SetCardPositions();
    }

    public void AddCardToHand(Card card)
    {
        cards.Add(card);
        SetCardPositions();
    }

}
