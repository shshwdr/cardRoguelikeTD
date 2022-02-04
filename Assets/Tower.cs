using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower:MonoBehaviour,IClickable
{
    public string type;
    public TowerInfo towerInfo;
    public Transform shootPoint;

    float currentCoolDown = 0;

    public  GameObject bulletPrefab;
    public GameObject renderObject;


    public GameObject selections;

    public SelectableTowerCell[] selectableCells;

    public void upgradeTower(DetailTowerInfo _info)
    {
        towerInfo.detailInfo = _info;
    }

    private void Awake()
    {

        selectableCells = GetComponentsInChildren<SelectableTowerCell>();
        selections = selectableCells[0].transform.parent.gameObject;
    }

    private void Start()
    {
        towerInfo = TowerManager.Instance.towerDict[type];
        TowerManager.Instance.BuildTower(this);
        hideInfo();
    }

    private void Update()
    {
        currentCoolDown += Time.deltaTime;
        if (currentCoolDown >= towerInfo.attackTime)
        {
            currentCoolDown = 0;
            attack();
        }
    }



    protected virtual void attack()
    {

    }


    public void showInfo()
    {
        selections.SetActive(true);
        //renderer.color = new Color(1, 1, 1, 0.5f);

        int i = 0;
        //foreach (var info in TowerManager.Instance.towerDict.Values)
        //{
        //    if (info.placeType == type && info.isUnlocked == 1)
        //    {
        //        if (i >= selectableCells.Length)
        //        {
        //            Debug.LogError("too many items but not enough cells, plan to add item " + i.ToString() + " but only have cell count " + selectableCells.Length.ToString());
        //        }
        //        selectableCells[i].gameObject.SetActive(true);
        //        selectableCells[i].GetComponent<SelectableCell>().init(info, this);
        //        i++;
        //    }
        //}

        foreach(var info in TowerManager.Instance.towerDetailsDict[towerInfo.name].Values)
        {
            if(info.levelTier == towerInfo.levelTier + 1)
            {
                selectableCells[i].gameObject.SetActive(true);
                selectableCells[i].initUpgrade(info, this);
                i++;
            }
        }

        selectableCells[i].gameObject.SetActive(true);
        selectableCells[i].initCancel(towerInfo, this);
        i++;
        for (; i < selectableCells.Length; i++)
        {
            selectableCells[i].gameObject.SetActive(false);

        }


    }
    public void hideInfo()
    {
        selections.SetActive(false);


       // renderer.color = new Color(0, 0, 0, 0);
    }
}