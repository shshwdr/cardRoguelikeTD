using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool useTarget = true;
    public Transform target;
    public Vector3 targetPosition;
    protected float damage;
    public float speed = 3f;
    public float hitDistance = 0.3f;
    public GameObject explosion;
    public bool useExplosionAnimation = false;
    protected float range;
    public int bulletType = 0;//0:single 1 area
    protected string buff;
    protected int towerLevel;
    protected Tower shootTower;
    public bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public virtual void init(Tower tower, Transform t)
    {
        damage = tower.damage;
        range = tower.spawnRange;
        speed = tower.spawnMoveSpeed;
        buff = tower.buff;
        towerLevel = tower.level;
        target = t;
        shootTower = tower;
        if (!useTarget)
        {
            targetPosition = target.position;
        }
    }

    public virtual void init(AreaDamageSpell spell, Vector3 position)
    {

        damage = spell.cardInfo.damage;
        range = spell.cardInfo.range;
        speed = spell.cardInfo.speed;
        buff = spell.cardInfo.buff;
        //towerLevel = tower.level;
        targetPosition = position;
        //target = t;
        ////shootTower = tower;
        //if (!useTarget)
        //{
        //    targetPosition = target.position;
        //}
    }
    // Update is called once per frame
    void Update()
    {
        if (useTarget)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            targetPosition = target.position;
        }
        var dir = (targetPosition - transform.position).normalized;
        transform.Translate(dir * speed*Time.deltaTime);
        if((targetPosition - transform.position).sqrMagnitude <= hitDistance)
        {
            hit();
        }
    }

    protected void hit()
    {
        if(bulletType == 0)
        {

            var hpObject = target.GetComponent<HPCharacterController>();
            if (hpObject)
            {
                hpObject.getDamage(damage, shootTower);
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
            foreach (var enemyC in colliders)
            {
                var enemy = enemyC.GetComponent<Customer>();
                if (enemy)
                {
                    enemy.getDamage(damage, shootTower);
                    if (buff!=null && buff!="")
                    {

                        enemy.applyBuff(new Dictionary<string, int>() { { buff, towerLevel } });
                    }
                }
                else
                {
                    Debug.LogError("customer not existed on " + enemy);
                }
            }
        }

        if (useExplosionAnimation)
        {
            GetComponentInChildren<Animator>().SetTrigger("explode");
            hasHit = true;
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(range / Utils.gridSize * 2, 0.7f));
            seq.PrependInterval(0.3f);
            //seq.Append(transform.DOScale(1.5f, 1f));

            Destroy(gameObject,2.1f);
        }
        else
        {

            var explo = Instantiate(explosion, transform.position, Quaternion.identity);
            explo.transform.localScale = Vector3.one * range * 2;
            Destroy(explo, 0.2f);
            Destroy(gameObject);
        }
    }
}
