using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectableTowerCell : MonoBehaviour
{

    public TMP_Text nameLabel;
    public TMP_Text descLabel;
    public TowerInfo info;
    public DetailTowerInfo dInfo;
    protected IClickable overlay;
    bool isCancel = false;
    bool isUpgrade = false;
    public void buildTower()
    {
        if (isCancel)
        {
            Destroy(overlay.gameObject);
            Inventory.Instance.addCoin(info.sellPrice);
        }else if (isUpgrade)
        {
            overlay.gameObject.GetComponent<Tower>().upgradeTower(dInfo);
            Inventory.Instance.consumeCoin(dInfo.price);
        }
        else
        {
            if (Inventory.Instance.canConsumeCoin(info.price))
            {

                var prefab = Resources.Load<GameObject>("tower/" + info.name);
                var go = Instantiate(prefab, MouseManager.Instance.previousSelected.transform.position, Quaternion.identity);
                Inventory.Instance.consumeCoin(info.price);
            }
        }
        overlay.hideInfo();
    }

    public void initAll()
    {
        isCancel = false;
        isUpgrade = false;
    }
    public void init(TowerInfo _info, IClickable _overlay)
    {
        initAll();
           info = _info;
        overlay = _overlay;

        nameLabel.text = info.displayName + " " + info.price.ToString();
        descLabel.text = info.description;
    }


    public void initCancel(TowerInfo _info, IClickable _overlay)
    {
        initAll();
        isCancel = true;
        info = _info;
        overlay = _overlay;

        nameLabel.text = "移除";
        descLabel.text = info.sellPrice.ToString();
    }

    public void initUpgrade(DetailTowerInfo _info, IClickable _overlay)
    {

        initAll();
        isUpgrade = true;
        dInfo = _info;
        overlay = _overlay;
        nameLabel.text = dInfo.displayName+" "+dInfo.price;
        descLabel.text = dInfo.description;
    }
}
