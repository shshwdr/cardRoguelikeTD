using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectRange : MonoBehaviour
{
    DraggableItem parentItem;
    // Start is called before the first frame update
    void Start()
    {
        parentItem = GetComponentInParent<DraggableItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherItem = other.GetComponent<DraggableItem>();
        if (otherItem && otherItem.isMainItem != parentItem.isMainItem)
        {
            //GetComponent<Renderer>().enabled = true;
            parentItem.affectedItems.Add(otherItem);
            otherItem.affectedItems.Add(parentItem);
            if (otherItem.isDragging)
            {

                parentItem.showActiveOverlay();
            }
            else
            {
                otherItem.showActiveOverlay();
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        var otherItem = other.GetComponent<DraggableItem>();
        if (otherItem && otherItem.isMainItem != parentItem.isMainItem)
        {
            if (!parentItem.affectedItems.Contains(otherItem))
            {
                Debug.LogError("item does not exist in affected items " + otherItem);
                return;
            }
            if (!otherItem.affectedItems.Contains(parentItem))
            {
                Debug.LogError("reverse item does not exist in affected items " + otherItem);
                return;
            }
            otherItem.affectedItems.Remove(parentItem);
            parentItem.affectedItems.Remove(otherItem);
            if (otherItem.isDragging)
            {

                parentItem.hideOverlay();
            }
            else
            {
                otherItem.hideOverlay();
            }

        }
    }

    public void hideEffectOnAffectedItems()
    {
        GetComponent<Renderer>().enabled = false;
        
    }

    public void showEffectOnAffectedItems()
    {
        foreach (var item in parentItem.affectedItems)
        {
            item.showActiveOverlay();

        }
    }
}
