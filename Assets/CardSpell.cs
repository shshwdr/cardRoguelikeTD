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
    void Start()
    {

        cardInfo = CardManager.Instance.getCardInfo(type);

        renderObject.transform.localScale = Vector3.one * Utils.gridSize * cardInfo.range;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
