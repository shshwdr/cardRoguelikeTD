using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower:CardEffect,IClickable
{
    public CardInfo towerInfo;
    public int level = 1;
    

    public int imageCount { get { return Resources.LoadAll<Sprite>("TowerArt/" + type).Length; } }
    public Sprite image { get { return Resources.Load<Sprite>("TowerArt/" + type+"/"+ Mathf.Min(imageCount, level)); } }

    public Tower DeepCopy()
    {
        Tower sc = new Tower();

        sc.towerInfo = this.towerInfo;
        sc.level = this.level;

        return sc;
    }

    //public Transform shootPoint;

    //float currentCoolDown = 0;

    //public  GameObject bulletPrefab;


    //public GameObject selections;

    //public SelectableTowerCell[] selectableCells;

    //public void upgradeTower(DetailTowerInfo _info)
    //{
    //    towerInfo.detailInfo = _info;
    //}

    protected override void Awake()
    {
        base.Awake();
        //selectableCells = GetComponentsInChildren<SelectableTowerCell>();
        //selections = selectableCells[0].transform.parent.gameObject;
        
    }

    public void upgrade()
    {
        level++;
        GetComponent<TowerInfoController>().init(this);
        GetComponent<DraggableItem>().renderer.sprite = image;
    }

    private void Start()
    {
        towerInfo = CardManager.Instance.getCardInfo(type);
        TowerManager.Instance.BuildTower(this);
        hideInfo();

        GetComponentInChildren<TowerInfoController>().init(this);
        GetComponent<DraggableItem>().renderer.sprite = image;
    }

    public float range
    {
        get
        {
            return rangeByLevel(level);
        }
    }
    public float nextRange {
        get
        {
            return rangeByLevel(level + 1);
        }
    }

    public bool canAttackFlying
    {
        get
        {
            return towerInfo.canAttackFly;
        }
    }

    public bool canAttackGround
    {
        get
        {
            return towerInfo.canAttackGround;
        }
    }

    public float nextRangeDiff
    {
        get
        {
            return rangeByLevel(level+1) - rangeByLevel(level);
        }
    }

    public float rangeByLevel(int l)
    {
        return towerInfo.range + towerInfo.rangeIncreaseByLevel * towerInfo.range * (l-1);
    }

    public float attackTime 
    {
        get
        {
            return speed == 0? speed:1f/ speed;
        }
    }

    public float speed {

        get
        {
            return speedByLevel(level);
        }
    }

    public float nextSpeedDiff
    {
        get
        {
            return speedByLevel(level+1) - speedByLevel(level);
        }
    }

    public float speedByLevel(int l)
    {
        return towerInfo.speed + towerInfo.speedIncreaseByLevel * towerInfo.speed * (l - 1);
    }


    public float spawnRange
    {
        get
        {
            return towerInfo.spawnRange;
        }
    }
    public float spawnMoveSpeed
    {
        get
        {
            return towerInfo.spawnMoveSpeed;
        }
    }


    public float damage
    {
        get
        {
            return damageByLevel(level);
        }
    }

    public float nextDamageDiff {
        get
        {
            return damageByLevel(level+1) - damageByLevel(level);
        }
    }

    public string buff
    {
        get
        {
            return towerInfo.buff;
        }
    }
    public int damageByLevel(int l)
    {
        return (int)( towerInfo.damage + towerInfo.damageIncreaseByLevel * towerInfo.damage * (l - 1));
    }


    private void Update()
    {
        //if (!isActivated)
        //{
        //    return;
        //}
        //currentCoolDown += Time.deltaTime;
        //if (currentCoolDown >= towerInfo.attackTime)
        //{
        //    currentCoolDown = 0;
        //    attack();
        //}
    }



    protected virtual void attack()
    {

    }


    public void showInfo()
    {
        //selections.SetActive(true);
        ////renderer.color = new Color(1, 1, 1, 0.5f);

        //int i = 0;
        ////foreach (var info in TowerManager.Instance.towerDict.Values)
        ////{
        ////    if (info.placeType == type && info.isUnlocked == 1)
        ////    {
        ////        if (i >= selectableCells.Length)
        ////        {
        ////            Debug.LogError("too many items but not enough cells, plan to add item " + i.ToString() + " but only have cell count " + selectableCells.Length.ToString());
        ////        }
        ////        selectableCells[i].gameObject.SetActive(true);
        ////        selectableCells[i].GetComponent<SelectableCell>().init(info, this);
        ////        i++;
        ////    }
        ////}

        //foreach(var info in TowerManager.Instance.towerDetailsDict[towerInfo.name].Values)
        //{
        //    if(info.levelTier == towerInfo.levelTier + 1)
        //    {
        //        selectableCells[i].gameObject.SetActive(true);
        //        selectableCells[i].initUpgrade(info, this);
        //        i++;
        //    }
        //}

        //selectableCells[i].gameObject.SetActive(true);
        //selectableCells[i].initCancel(towerInfo, this);
        //i++;
        //for (; i < selectableCells.Length; i++)
        //{
        //    selectableCells[i].gameObject.SetActive(false);

        //}


    }
    public void hideInfo()
    {
        //selections.SetActive(false);


       // renderer.color = new Color(0, 0, 0, 0);
    }
}