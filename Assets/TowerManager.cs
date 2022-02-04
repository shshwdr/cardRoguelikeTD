using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfo
{
    public string name;
    public string displayName;
    public string description;
}

public class TowerInfo : BaseInfo
{
    public string placeType;
    public int isUnlocked;
    public DetailTowerInfo detailInfo;

    public int spentPrice;
    public string description { get { return detailInfo.description; } }
    public float attackTime { get { return detailInfo.attackTime; } }
    public float attackDamage { get { return detailInfo.attackDamage; } }
    public int price { get {
            string test = name;
            
            return (int)(detailInfo.price * Mathf.Pow(TowerManager.Instance.buildTowerPenalty,(1+ TowerManager.Instance.builtTowerDict[name])));
        } }
    public int sellPrice { get { return (int)(spentPrice * TowerManager.Instance.sellTowerPenalty); } }
    public float levelTier { get { return detailInfo.levelTier; } }
    public float range { get { return detailInfo.range; } }
}

public class DetailTowerInfo: BaseInfo
{
    public int levelTier;
    public int price;
    public float attackTime;
    public float attackDamage;
    public float range;
}

public class SingleTowerInfo: DetailTowerInfo
{

}

public class GuideTowerInfo : DetailTowerInfo {

    public float blockTime;
    public int blockCount;
}

public class BankTowerInfo : DetailTowerInfo {

    public float blockTime;
    public int blockCount;
    public float moneyRefill;
}




public class TowerManager : Singleton<TowerManager>
{

    public float buildTowerPenalty { get { return 1.25f; } }
    public float sellTowerPenalty { get { return 0.5f; } }

    public Dictionary<string, int> builtTowerDict = new Dictionary<string, int>();
    public Dictionary<string, TowerInfo> towerDict = new Dictionary<string, TowerInfo>();
    public Dictionary<string, Dictionary<string, DetailTowerInfo>> towerDetailsDict = new Dictionary<string, Dictionary<string, DetailTowerInfo>>();
    private void Awake()
    {
        var towers = CsvUtil.LoadObjects<TowerInfo>("Tower");
        foreach (var info in towers)
        {
            //very bad code
            switch (info.name) {
                case "single":
                case "bomb":
                case "fridge":
                    towerDetailsDict[info.name] = new Dictionary<string, DetailTowerInfo>();
                    var towerDetails = CsvUtil.LoadObjects<SingleTowerInfo>("Towers/" + info.name);
                    foreach (var dInfo in towerDetails)
                    {
                        if (info.detailInfo == null)
                        {
                            info.detailInfo = dInfo;
                        }
                        towerDetailsDict[info.name][dInfo.name] = dInfo;
                    }
                    break;
                case "guide":

                    towerDetailsDict[info.name] = new Dictionary<string, DetailTowerInfo>();
                    var towerDetails2 = CsvUtil.LoadObjects<GuideTowerInfo>("Towers/" + info.name);
                    foreach (var dInfo in towerDetails2)
                    {
                        if (info.detailInfo == null)
                        {
                            info.detailInfo = dInfo;
                        }
                        towerDetailsDict[info.name][dInfo.name] = dInfo;
                    }
                    break;
                case "bank":

                    towerDetailsDict[info.name] = new Dictionary<string, DetailTowerInfo>();
                    var towerDetails3 = CsvUtil.LoadObjects<BankTowerInfo>("Towers/" + info.name);
                    foreach (var dInfo in towerDetails3)
                    {
                        if (info.detailInfo == null)
                        {
                            info.detailInfo = dInfo;
                        }
                        towerDetailsDict[info.name][dInfo.name] = dInfo;
                    }
                    break;
            }
            builtTowerDict[info.name] = 0;
            towerDict[info.name] = info;

        }
    }

    public void BuildTower(Tower go)
    {
        var info = go.towerInfo;
        info.spentPrice = info.price;
        builtTowerDict[info.name]++;

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
