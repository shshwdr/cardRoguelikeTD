using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShootTower : AttackByTime
{
    public int shootCount = 1;

    public GameObject bulletPrefab;
    protected override void Start()
    {
        base.Start();
        if (!bulletPrefab)
        {
            bulletPrefab = Resources.Load<GameObject>("bullet/" + tower.type);
        }
    }
    int shoot(Customer cus,int currentShootCount)
    {
        var go = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        go.GetComponent<Bullet>().init(tower, cus.transform);
        //cus.getDamage(towerInfo.attackDamage);
        currentShootCount++;
        return currentShootCount;
    }

    protected override void attack()
    {
        base.attack();
        int currentShootCount = 0;

        //check target 
        if (MouseManager.Instance.currentFocusTarget)
        {
            var focusCustomer = MouseManager.Instance.currentFocusTarget.GetComponent<Customer>();
            if ((focusCustomer.transform.position - shootPoint.position).magnitude <= tower.range && focusCustomer.canBeDamaged(tower))
            {
                currentShootCount = shoot(focusCustomer, currentShootCount);
            }
        }

        //check all the others
        foreach (var cus in CustomerManager.Instance.getAllCustoemrsWithMoney())
        {
            if (currentShootCount >= shootCount)
            {
                return;
            }
            if(cus == null)
            {
                continue;
            }
            if ((cus.transform.position - shootPoint.position).magnitude <= tower.range && cus.canBeDamaged(tower))
            {
                currentShootCount = shoot(cus, currentShootCount);
            }
        }
    }

}
