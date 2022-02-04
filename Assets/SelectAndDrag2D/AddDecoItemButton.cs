using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDecoItemButton : AddMainItemButton
{

    public override bool CanPurchaseItem()
    {
        return hasEnoughRequirementItems();// && BuildModeManager.Instance.currentRoom.canAddNewItem();
    }

}
