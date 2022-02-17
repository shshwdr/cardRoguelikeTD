using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoController : MonoBehaviour
{
    public GameObject towerInfo;
    public Transform rangeCircle;

    public Text nameLabel;
    public Text levelLabel;
    public Text damageLabel;
    public Text speedLabel;
    public Text rangeLabel;


    public GameObject upgradeInfo;
    public Text upgradeLevelLabel;
    public Text upgradeDamageLabel;
    public Text upgradeSpeedLabel;
    public Text upgradeRangeLabel;
    public Transform upgradeRangeCircle;

    public bool canUpgradeWith(Tower tower)
    {
        if (tower.type == GetComponent<Tower>().type)
        {
            return true;
        }
        return false;
    }

    public void init(Tower tower)
    {
        if(!tower || tower.towerInfo == null||!nameLabel)
        {
            Debug.Log("???");
        }
        nameLabel.text = "name: "+tower.towerInfo.displayName;
        var range = tower.range;
        rangeCircle.localScale = Vector3.one* range * 2;
        rangeLabel.text = "range: " + range.ToString("F2");
        levelLabel.text = "level: " + tower.level.ToString();
        damageLabel.text = "damage: " + tower.damage.ToString();
        speedLabel.text = "speed: " + tower.speed.ToString("F2");
        upgradeInfo.SetActive(false);

    }

    public void showInfo()
    {
        towerInfo.SetActive(true);
        //upgradeInfo.SetActive(false);
    }

    public void showUpgradeInfo(Tower tower)
    {

        towerInfo.SetActive(true);
        upgradeInfo.SetActive(true);


        upgradeLevelLabel.text = "+1";
       if(tower.nextDamageDiff > 0)
        {
            upgradeDamageLabel.text = "+"+ tower.nextDamageDiff.ToString();
        }
        else
        {
            upgradeDamageLabel.text = "";
        }

        if (tower.nextRangeDiff > 0)
        {
            upgradeRangeLabel.text = "+" + tower.nextRangeDiff.ToString("F2");
            upgradeRangeCircle.transform.localScale = Vector3.one * tower.nextRange * 2;
        }
        else
        {
            upgradeRangeLabel.text = "";
            upgradeRangeCircle.transform.localScale = Vector3.zero;
        }

        if (tower.nextSpeedDiff > 0)
        {
            upgradeSpeedLabel.text = "+" + tower.nextSpeedDiff.ToString("F2");
        }
        else
        {
            upgradeSpeedLabel.text = "";
        }
    }

    public void hideUpgradeInfo()
    {
        upgradeInfo.SetActive(false);
    }

    public void hideInfo()
    {
        towerInfo.SetActive(false);
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
