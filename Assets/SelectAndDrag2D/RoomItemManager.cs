using LitJson;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeyValueBase
{
    public string key;
    public int amount;

}

public class RoomDecorationInfo : RoomItemInfo {
    public float affectRadius;
}

public class InfoWithRequirementBase : InfoBase
{
    public KeyValueBase[] requireResources;
}
public class RoomItemInfo : InfoWithRequirementBase
{
    public string catelog;
    public int earning;
    public bool isUnlocked;

}
public class AllRoomItemInfo
{
    public List<RoomItemInfo> allMainItem;
    public List<RoomDecorationInfo> allDecorations;
}
public class RoomItemManager : Singleton<RoomItemManager>
{
    public float maxAffectRange = 0;
    public Dictionary<string, RoomItemInfo> mainItemInfoDict = new Dictionary<string, RoomItemInfo>();
    public Dictionary<string, RoomItemInfo> decoItemInfoDict = new Dictionary<string, RoomItemInfo>();
    public Dictionary<string, List<DraggableItem>> items = new Dictionary<string, List<DraggableItem>>();
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/item").text;
        var allNPCs = JsonMapper.ToObject<AllRoomItemInfo>(text);
        foreach (RoomItemInfo info in allNPCs.allMainItem)
        {
            mainItemInfoDict[info.name] = info;
        }
        foreach (RoomDecorationInfo info in allNPCs.allDecorations)
        {
            decoItemInfoDict[info.name] = info;
            maxAffectRange = Mathf.Max(info.affectRadius, maxAffectRange);
        }
    }

    public bool canBuildItem(DraggableItem item)
    {

        var roomCollider = item.placeCollider;
        foreach (var it in items.Values)
        {
            foreach(var r in it)
            {
                if(r!= item)
                {
                    if (r.placeCollider.bounds.Intersects(roomCollider.bounds))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public void addItem(DraggableItem item)
    {
        if (!items.ContainsKey(item.catelog))
        {
            items[item.catelog] = new List<DraggableItem>();
        }

        items[item.catelog].Add(item);
        //EventPool.Trigger("changeItem");
    }


    public void removeItem(DraggableItem item)
    {
        items[item.catelog].Remove(item);
        //ShowNavmesh.Instance.ShowMesh();
    }

    public List<DraggableItem> availableBedItem()
    {
        List<DraggableItem> res = new List<DraggableItem>();
        if (!items.ContainsKey("bed"))
        {
            return res;
        }
        foreach (DraggableItem item in items["bed"])
        {
            if (!item.occupied)
            {
                res.Add(item);
            }
        }
        return res;
    }
}
