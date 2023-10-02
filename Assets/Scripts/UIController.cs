using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; private set; }
    public Button drawCardButton;

    [SerializeField] private TextMeshProUGUI playerMana;
    [SerializeField] private GameObject manaWarning;
    [SerializeField] private float manaWarningDisplayTime = 2f;
    private float manaWarningTimer;

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
        UpdateManaWarning();
    }

    public void SetPlayerMana(int mana)
    {
        playerMana.text = $"Mana: {mana}";
    }

    public void ShowManaWarning()
    {
        manaWarning.SetActive(true);
        manaWarningTimer = manaWarningDisplayTime;
    }

    public void UpdateManaWarning()
    {
        if (manaWarningTimer <= 0) return;

        manaWarningTimer -= Time.deltaTime;
        if (manaWarningTimer <= 0)
        {
            manaWarning.SetActive(false);
        }
    }

    public void DrawExtraCard()
    {
        DeckController.instance.DrawExtraCard();
    }

}
