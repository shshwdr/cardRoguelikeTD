using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageSpell : CardSpell
{

    // Start is called before the first frame update
   

    public override void activate()
    {
        base.activate();
        //do damage
        Collider2D[] colliders = Physics2D.OverlapCircleAll(cardHappenCenter.position, cardInfo.range* Utils.gridSize/2f, LayerMask.GetMask("Enemy"));
        foreach(var enemyC in colliders)
        {
            var enemy = enemyC.GetComponent<Customer>();
            if (enemy)
            {
                enemy.getDamage(cardInfo.damage);
            }
            else
            {
                Debug.LogError("customer not existed on " + enemy);
            }
        }
        Destroy(gameObject, 0.1f);
        //disappear about 1 sec
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
