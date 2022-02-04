using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TollTower : Tower
{
    public float jumpHeight = 0.1f;
    public float jumpTime = 0.3f;
    protected override void attack()
    {
        base.attack();
        int currentShootCount = 0;
        var customers = new List<Customer>( CustomerManager.Instance.getAllCustomers());
        foreach (var cus in customers)
        {
            if ((cus.transform.position - shootPoint.position).magnitude <= towerInfo.range)
            {
                renderObject.transform.DOLocalJump(Vector3.zero, jumpHeight,1 , jumpTime);
                //var go = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                //go.GetComponent<Bullet>().init(towerInfo.attackDamage, cus.transform);
                cus.getDamage(towerInfo.attackDamage);

                //currentShootCount++;
                //if (currentShootCount >= shootCount)
                //{
                //    break;
                //}
            }
        }
    }
}
