using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyTower : AttackByTime
{
    protected override void attack()
    {
        base.attack();
        Inventory.Instance.addCoin((int)tower.damage);
    }
}
