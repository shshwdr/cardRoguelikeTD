using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DraggableItem : Draggable
{
    public bool isMainItem;
    public bool occupied;
    public AffectRange effectRangeItem;

    public List<DraggableItem> affectedItems = new List<DraggableItem>();

    public string catelog { get { return ((RoomItemInfo)info).catelog; } }

    
    public void customerOccupy()
    {
        occupied = true;
    }

    public void customerRelease()
    {
        occupied = false;
    }

    public void Init(string t, InfoBase i, bool ism)
    {
        base.Init(t,i);
        isMainItem = ism;
        if (!isMainItem)
        {
            //update radius
            float radius = ((RoomDecorationInfo)info).affectRadius;
            effectRangeItem.transform.localScale = new Vector3(radius, 1, radius);
        }
        else
        {
            //use max radius
            //float radius = 0;// RoomItemManager.Instance.maxAffectRange;
            //effectRangeItem.GetComponent<Renderer>().enabled = false;
            //effectRangeItem.transform.localScale = new Vector3(radius, 1, radius);
        }
    }

    protected override void Update()
    {
        base.Update();

    }
    public override bool canBuildItem()
    {
        return true;
        //return RegionManager.Instance.currentRegion.canPlaceRoom(this);
    }
    protected override void build()
    {
        if (effectRangeItem)
        {
            hideOverlay();
            effectRangeItem.hideEffectOnAffectedItems();
        }
        foreach (var item in affectedItems)
        {
            item.hideOverlay();
            if (item.effectRangeItem)
            {
                item.hideOverlay();
                item.effectRangeItem.hideEffectOnAffectedItems();
            }
        }
        if (!isBuilt)
        {
            RoomItemManager.Instance.addItem(this);

            consumeRequirements();
        }
        isBuilt = true;
        //StartCoroutine(renderNavAsync());
    }
    public override void removeDragItem()
    {
        if (isBuilt)
        {
            RoomItemManager.Instance.removeItem(this);
            addBackRequirements();
            transform.position = new Vector3(10000, transform.position.y, 10000);
            //StartCoroutine(renderNavAsync());
        }
        //hacky, otherwise nav wont update correctly..
        Destroy(gameObject,0.1f);
    }
        


    //public override void cancelDragItem()
    //{
    //    base.cancelDragItem();
    //    if (isBuilt)
    //    {
    //        //pop up
    //        removeDragItem();
    //    }
    //}
}
