using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTower : Tower
{
    int blockCount = 3;
    int currentBlockCount = 0;
    float blockTime = 6f;

    protected override void attack()
    {
        base.attack();
        if (currentBlockCount >= blockCount)
        {
            return;
        }
        foreach (var cus in CustomerManager.Instance.getAllCustoemrsWithMoney())
        {
            if ((cus.transform.position - shootPoint.position).magnitude <= towerInfo.range)
            {
                //find a better way!
                if (cus.getBlocked(this))
                {
                    currentBlockCount++;
                    if (currentBlockCount >= blockCount)
                    {
                        break;
                    }
                }
            }
        }
    }


    public void releaseCustomer(Customer cus)
    {
        currentBlockCount -= 1;
    }

    public float getBlockTime()
    {
        return blockTime;
    }
}
