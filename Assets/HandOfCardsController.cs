using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandOfCardsController : MonoBehaviour
{
    HandCardCell[] cards;
    BattleCardManager battleCardManager;
    public Text redrawCooldownLabel;
    public Button redrawButton;


    public Text cardDetailName;
    public Text cardDetailDesc;
    public GameObject cardDetail;
    private void Awake()
    {
        cards = GetComponentsInChildren<HandCardCell>(true);
        battleCardManager = BattleCardManager.Instance;
        EventPool.OptIn("handCardsUpdate", updateCards);
        EventPool.OptIn("redrawTimeUpdate", updateRedrawCooldown); 
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showCardDetail(Card c)
    {
        cardDetail.SetActive(true);
        cardDetailName.text = c.cardName;
        cardDetailDesc.text = c.cardDesc;
    }

    public void hideCardDetail()
    {
        cardDetail.SetActive(false);
    }

    public void updateRedrawCooldown()
    {
        int timeLeft = (int)battleCardManager.getRedrawTimer();
        if (timeLeft > 0)
        {
            redrawCooldownLabel.gameObject.SetActive(true);

            redrawCooldownLabel.text = battleCardManager.getRedrawTimer().ToString();
            redrawButton.interactable = false;
        }
        else
        {
            redrawCooldownLabel.gameObject.SetActive(false);
            redrawButton.interactable = true;
        }
    }

    public void updateCards()
    {
        int i = 0;

        for(; i< battleCardManager.CardsOnHand.Count; i++)
        {
            cards[i].gameObject.SetActive(true);
            cards[i].init(battleCardManager.CardsOnHand[i]);
        }
        for (; i < cards.Length; i++)
        {
            cards[i].gameObject.SetActive(false);

        }
    }

    public void redrawCards()
    {
        BattleCardManager.Instance.redrawHand();
    }
}
