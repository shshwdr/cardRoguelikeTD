using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardCell : MonoBehaviour
{
    Card card;
    public Text cardNameLabel;
    public Text cardCostLabel;

    public void init(Card c)
    {
        card = c;
        cardNameLabel.text = card.cardName;
        cardCostLabel.text = card.cost.ToString();
    }

    public void onPointerDown()
    {
        if (Inventory.Instance.canConsumeCoin(card.cost))
        {
            //spawn different things
            spawnTower();
        }
        else
        {
            PixelCrushers.DialogueSystem.DialogueManager.ShowAlert("Not Enough Coins");
        }
    }

    public void spawnTower()
    {
        var prefab = Resources.Load<GameObject>("tower/"+ card.cardInfo.name);
        GameObject spawnInstance = Instantiate(prefab);
        spawnInstance.GetComponent<CardEffect>().cost = card.cost;
        spawnInstance.GetComponent<CardEffect>().card = card;
        if (spawnInstance.GetComponent<DraggableItem>())
        {
            MouseManager.Instance.startDragItem(spawnInstance);
        }
        spawnInstance.name = card.cardInfo.name;
        //spawnInstance.GetComponent<DraggableItem>().Init(prefab.name, itemInfo, isMainItem);

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
