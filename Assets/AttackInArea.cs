using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInArea : AttackByTime
{
    public Collider2D attackArea;
    public SpriteRenderer sprite;
    protected override void Awake()
    {
        base.Awake();
        if (!attackArea)
        {
            attackArea = GetComponent<Collider2D>();
        }
    }
    protected override void attack()
    {
        base.attack();
        var result = Physics2D.OverlapCircleAll(shootPoint.position,  (attackArea as CircleCollider2D).radius*attackArea.transform.localScale.x);
        bool hitted = false;
        foreach(var r in result)
        {
            var customer = r.GetComponent<Customer>();
            if (r.GetComponent<Customer>() && r.GetComponent<Customer>().canBeDamaged(tower))
            {
                if (tower.damage > 0)
                {
                    r.GetComponent<Customer>().getDamage(tower.damage, tower);
                }
                if (tower.towerInfo.buff != null)
                {
                    if (tower.towerInfo.buffPercent > 0)
                    {
                        if (Random.Range(0, 100) <= tower.towerInfo.buffPercent)
                        {
                            customer.applyBuff(new Dictionary<string, int>(){ { tower.towerInfo.buff,tower.level}},tower);
                        }
                    }
                }
                hitted = true;
            }
        }
        if (hitted)
        {

            sprite.DOFade(1, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
