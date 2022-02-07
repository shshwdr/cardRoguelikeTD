
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect:MonoBehaviour
{
    public int cost;
    public Card card;

    [SerializeField] string cardType;
    public string type { get { return cardType.Length > 0 ? cardType : name; } }
    public bool isActivated = false;
    public GameObject renderObject;
    public virtual void activate()
    {
        isActivated = true;
    }


    public void consumeRequirements()
    {
        //InfoWithRequirementBase infoWithRequirement = (InfoWithRequirementBase)info;
        //var requirements = infoWithRequirement.requireResources;
        //foreach(var req in requirements)
        //{
        //    Inventory.Instance.consumeItem(req.key, req.amount);
        //}
        Inventory.Instance.consumeCoin(cost);
        BattleCardManager.Instance.useCard(card);
    }

    protected virtual  void Awake()
    {
        if (!renderObject)
        {
            renderObject = transform.Find("render").GetChild(0).gameObject;
        }
    }

}