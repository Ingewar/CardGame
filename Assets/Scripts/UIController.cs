using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }

    [SerializeField] private TextMeshProUGUI playerMana;

    void Awake()
    {
        if (instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerMana(int mana)
    {
        playerMana.text = $"Mana: {mana}";
    }
}
