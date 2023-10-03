using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance { get; private set; }

    public int startMana = 4, maxMana = 12;
    public int playerMana;
    [SerializeField] private int startNumberOfCards = 3;

    public enum BattleState
    {
        PlayerTurn,
        PlayerCardsAttack,
        EnemyTurn,
        EnemyCardsAttack
    }

    public BattleState battleState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        playerMana = startMana;
        UIController.instance.SetPlayerMana(playerMana);

        DeckController.instance.DrawCards(startNumberOfCards);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceTurn();
        }
    }

    public void SpendMana(int mana)
    {
        playerMana -= mana;
        if (playerMana < 0)
        {
            playerMana = 0;
        }

        UIController.instance.SetPlayerMana(playerMana);
    }

    public void AdvanceTurn()
    {
        switch (battleState)
        {
            case BattleState.PlayerTurn:
                battleState = BattleState.PlayerCardsAttack;
                break;
            case BattleState.EnemyTurn:
                battleState = BattleState.EnemyCardsAttack;
                break;
            case BattleState.PlayerCardsAttack:
                battleState = BattleState.EnemyTurn;
                break;
            case BattleState.EnemyCardsAttack:
                battleState = BattleState.PlayerTurn;
                break;
        }
    }
}
