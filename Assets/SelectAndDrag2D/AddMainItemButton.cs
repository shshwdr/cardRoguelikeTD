using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMainItemButton : BuildItemButton
{
    public bool isMainItem;
    AllResourcesRequiredController allResourceRequiredController;

    private void Awake()
    {
        allResourceRequiredController = GetComponentInChildren<AllResourcesRequiredController>();

    }

    public override void init(InfoBase info)
    {
        base.init(info);
        updateRequirement();
        EventPool.OptIn("inventoryChanged", updateRequirement);
    }

    public void updateRequirement()
    {
        if (allResourceRequiredController)
        {
            allResourceRequiredController.init(itemInfo);
        }
    }
    public override string itemType()
    {
        return "item";
    }
    public override void SpawnItem()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //PlantsManager.Instance.shadowCollider.gameObject.SetActive(true);
        GameObject spawnInstance = Instantiate(prefab);
        spawnInstance.GetComponent<DraggableItem>().Init(prefab.name,itemInfo, isMainItem);

        //MouseManager.Instance.startDragItem(spawnInstance);
    }
    public override bool CanPurchaseItem()
    {
        return hasEnoughRequirementItems();
    }


}
