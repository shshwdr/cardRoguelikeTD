using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Singleton<DeckManager>
{
    List<CardInfo> deckInfos = new List<CardInfo>();
    List<Card> currentDeck = new List<Card>();
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void createAndShuffleCards()
    {
        currentDeck.Clear();

        foreach(var cardinfo in getDeckInfos())
        {
            Card card = new Card(cardinfo);
            currentDeck.Add(card);
        }

        currentDeck.Shuffle();
    }
    public Card drawCard()
    {
        if(currentDeck.Count == 0)
        {
            createAndShuffleCards();
        }
        var firstCard = currentDeck[0];
        currentDeck.RemoveAt(0);
        return firstCard;
    }
    public List<CardInfo> getDeckInfos()
    {
        //
        if(currentDeck.Count == 0)
        {

            var characterInfo = CharacterManager.Instance.getCurrentCharacterInfo();
            var deck = characterInfo.initialDeck;
            foreach (var pair in deck)
            {
                for(int i = 0; i < pair.Value; i++)
                {
                    deckInfos.Add(CardManager.Instance.getCardInfo(pair.Key));
                }
            }
        }
        else
        {
            Debug.Log("deck is not empty");
        }
        return deckInfos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
