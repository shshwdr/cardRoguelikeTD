using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardInfo : BaseInfo
{
    public string cardType;
    public float cost;
    public int isUnlocked;
}


public class CardManager : Singleton<CardManager>
{
    public Dictionary<string, CardInfo> cardInfoDict = new Dictionary<string, CardInfo>();
    private void Awake()
    {
        var cards = CsvUtil.LoadObjects<CardInfo>("Card");
        foreach (var card in cards)
        {
            cardInfoDict[card.name] = card;
        }
    }
    public CardInfo getCardInfo(string n)
    {
        return cardInfoDict[n];
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
