using Sinbad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardInfo : BaseInfo
{
    public string cardType;
    public float cost;
    public int isUnlocked;
    public float damage;
    public float speed;
    public float spawnMoveSpeed;
    public float range;
    public float spawnRange;
    public List<string> tags;
    public string buff;


    public float damageIncreaseByLevel;
    public float speedIncreaseByLevel;
    public float rangeIncreaseByLevel;

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
            //if (card.speed != 0)
            //{
            //    card.speed = 1f / card.speed;
            //}
            card.range = card.range * Utils.gridSize;
            card.spawnRange = card.spawnRange * Utils.gridSize;
        }
    }
    public CardInfo getCardInfo(string n)
    {
        if (!cardInfoDict.ContainsKey(n))
        {
            Debug.LogError("card not exist " + n);
        }
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
