using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance { get; private set; }

    public int startMana = 4, maxMana = 12;
    public int playerMana;

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
    }

    // Update is called once per frame
    void Update()
    {

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
}
