using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController instance;

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
}
