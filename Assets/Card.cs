using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card
{
    public CardInfo cardInfo;
    public string cardName { get { return cardInfo.displayName; } }
    public string cardDesc { get { return cardInfo.description; } }
    public int cost { get { return (int)cardInfo.cost; } }
    public Card(CardInfo inf)
    {
        cardInfo = inf;
    }


}