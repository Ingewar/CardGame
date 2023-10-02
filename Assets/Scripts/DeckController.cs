using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController instance;

    [SerializeField] private Card cardPrefab;
    [SerializeField] private int drawCardCost = 2;
    [SerializeField] private float drawCardDelay = .25f;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private List<CardScriptableObject> deck = new List<CardScriptableObject>();
    private List<CardScriptableObject> activeDeck = new List<CardScriptableObject>();

    // Start is called before the first frame update
    void Start()
    {
        SetupDeck();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetupDeck()
    {
        activeDeck.Clear();

        List<CardScriptableObject> tempDeck = new List<CardScriptableObject>(deck);

        while (tempDeck.Count > 0)
        {
            int randomIndex = Random.Range(0, tempDeck.Count);
            CardScriptableObject randomCard = tempDeck[randomIndex];
            activeDeck.Add(randomCard);
            tempDeck.Remove(randomCard);
        }
    }

    public void DrawCard()
    {
        if (activeDeck.Count > 0)
        {
            Card newCard = Instantiate(cardPrefab, transform.position, transform.rotation);
            newCard.cardSO = activeDeck[0];
            activeDeck.RemoveAt(0);

            HandController.instance.AddCardToHand(newCard);
        }
    }

    public void DrawExtraCard()
    {
        if (BattleController.instance.playerMana >= drawCardCost)
        {
            BattleController.instance.SpendMana(drawCardCost);
            DrawCard();
        }
        else
        {
            UIController.instance.ShowManaWarning();
            UIController.instance.drawCardButton.gameObject.SetActive(false);
        }
    }

    public void DrawCards(int cardsAmount)
    {
        StartCoroutine(DrawCardsCo(cardsAmount));
    }

    private IEnumerator DrawCardsCo(int cardsAmount)
    {
        for (int i = 0; i < cardsAmount; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(drawCardDelay);
        }
    }
}
