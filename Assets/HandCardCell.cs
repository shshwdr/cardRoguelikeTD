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
        var prefab = Resources.Load<GameObject>("tower/"+ card.cardName);
        GameObject spawnInstance = Instantiate(prefab);
        spawnInstance.GetComponent<DraggableItem>().cost = card.cost;
        spawnInstance.GetComponent<DraggableItem>().card = card;
        //spawnInstance.GetComponent<DraggableItem>().Init(prefab.name, itemInfo, isMainItem);

        MouseManager.Instance.startDragItem(spawnInstance);
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
