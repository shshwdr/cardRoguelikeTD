using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardCell : MonoBehaviour
{
    Card card;
    public Text cardNameLabel;

    public void init(Card c)
    {
        card = c;
        cardNameLabel.text = card.cardName;
    }

    public void onPointerEnter()
    {
        GetComponentInParent<HandOfCardsController>().showCardDetail(card);
    }
    public void onPointerExit()
    {

        GetComponentInParent<HandOfCardsController>().hideCardDetail();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
