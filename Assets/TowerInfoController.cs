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



    public void init(Tower tower)
    {
        nameLabel.text = "name: "+tower.towerInfo.displayName;
        var range = tower.range;
        rangeCircle.localScale = Vector3.one* range * 2;
        rangeLabel.text = "range: " + range.ToString();
        levelLabel.text = "level: " + tower.level.ToString();
        damageLabel.text = "damage: " + tower.damage.ToString();
        speedLabel.text = "speed: " + tower.attackTime.ToString();
    }

    public void showInfo()
    {
        towerInfo.SetActive(true);
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
