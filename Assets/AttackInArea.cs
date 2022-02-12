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
        var result = Physics2D.OverlapCircleAll(shootPoint.position, attackArea.bounds.extents.magnitude);
        bool hitted = false;
        foreach(var r in result)
        {
            if (r.GetComponent<Customer>() && r.GetComponent<Customer>().canBeDamaged(tower))
            {
                r.GetComponent<Customer>().getDamage(tower.damage, tower);
                hitted = true;
            }
        }
        if (hitted)
        {

            sprite.DOFade(1, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
