using LitJson;
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HouseLevelInfo : InfoBase
{
    public int levelUpValue;
    public string nextLevel;
    public string[] unlockItems;
}
public class AllHouseLevel
{
    public List<HouseLevelInfo> levelup;
}
public class BathhouseLevelManager : Singleton<BathhouseLevelManager>
{
    public Dictionary<string, HouseLevelInfo> levelDict = new Dictionary<string, HouseLevelInfo>();
    int currentHappiness = 0;
    HouseLevelInfo currentlevel;
    private void Awake()
    {
        string text = Resources.Load<TextAsset>("json/Levelup").text;
        var allNPCs = JsonMapper.ToObject<AllHouseLevel>(text);
        foreach (HouseLevelInfo info in allNPCs.levelup)
        {
            levelDict[info.name] = info;
        }
        currentlevel = allNPCs.levelup[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        updateLevel();
    }

    public int getHappy()
    {
        return currentHappiness;
    }
    public string getLevel()
    {
        return currentlevel.displayName;
    }

    void updateLevel()
    {
        if (currentlevel.nextLevel!=null && currentlevel.levelUpValue<=currentHappiness)
        {
            currentlevel = levelDict[currentlevel.nextLevel];
            EventPool.Trigger("houseLevelChanged");
        }
    }

    public void addHappy(int amount)
    {
        currentHappiness += amount;
        updateLevel();
        EventPool.Trigger("happinessChanged");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
