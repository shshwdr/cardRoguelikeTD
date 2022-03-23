using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpell : CardEffect
{
    public CardInfo cardInfo;
    public Transform cardHappenCenter;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        if (!cardHappenCenter)
        {

            cardHappenCenter = GetComponent<Transform>();
        }
    }
    protected virtual void Start()
    {

        cardInfo = CardManager.Instance.getCardInfo(type);

        renderObject.transform.localScale = Vector3.one * cardInfo.range * 2;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
