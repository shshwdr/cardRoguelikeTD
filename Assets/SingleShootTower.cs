using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShootTower : AttackByTime
{
    public int shootCount = 1;

    protected override void attack()
    {
        base.attack();
        int currentShootCount = 0;
        foreach (var cus in CustomerManager.Instance.getAllCustoemrsWithMoney())
        {
            if(cus == null)
            {
                continue;
            }
            if ((cus.transform.position - shootPoint.position).magnitude <= tower.range)
            {
                var go = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                go.GetComponent<Bullet>().init(tower, cus.transform);
                //cus.getDamage(towerInfo.attackDamage);
                currentShootCount++;
                if (currentShootCount >= shootCount)
                {
                    break;
                }
            }
        }
    }

}
