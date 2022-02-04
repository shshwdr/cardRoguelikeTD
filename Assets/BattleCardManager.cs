using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardManager : Singleton<BattleCardManager>
{
    public int maxCardCountInHand = 5;
    public List<Card> CardsOnHand = new List<Card>();

    public float redrawCooldown = 10f;
    float redrawTimer = 0;

    public float getRedrawTimer()
    {
        return redrawTimer;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void clearHand()
    {
        CardsOnHand.Clear();
    }

    public void redrawHand()
    {
        clearHand();
        drawHand();
        redrawTimer = redrawCooldown;
    }

    public void drawHand()
    {
        for(int i = 0; i < maxCardCountInHand; i++)
        {
            CardsOnHand.Add(DeckManager.Instance.drawCard());
        }
        EventPool.Trigger("handCardsUpdate");
    }

    // Update is called once per frame
    void Update()
    {
        if (redrawTimer > 0)
        {
            redrawTimer -= Time.deltaTime;

            EventPool.Trigger("redrawTimeUpdate");
        }
    }
}
