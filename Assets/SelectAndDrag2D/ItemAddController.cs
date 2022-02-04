using System.Collections.Generic;

public class ItemAddController : SelectionController
{
    public bool isMainItem;
    protected override List<InfoBase> allItems()
    {
        var res = new List<InfoBase>();
        Dictionary<string, RoomItemInfo>.ValueCollection allInfos;
        if (isMainItem)
        {
            allInfos = RoomItemManager.Instance.mainItemInfoDict.Values;
        }
        else
        {

            allInfos = RoomItemManager.Instance.decoItemInfoDict.Values;
        }
        foreach (var info in allInfos)
        {
            if (info.isUnlocked)
            {
                res.Add(info);
            }
        }
        return res;
    }
}
