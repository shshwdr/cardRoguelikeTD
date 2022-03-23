using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageSpell : CardSpell
{
    public GameObject bulletPrefab;
    public bool shootFromHero = true;
    protected override void Start()
    {
        base.Start();
        if (!bulletPrefab)
        {
            bulletPrefab = Resources.Load<GameObject>("bullet/" + cardInfo.name);
        }
    }

    // Start is called before the first frame update

    void shoot(Vector3 position)
    {
        Transform endCharacter = GameObject.Find("endCharacter").transform;
        var go = Instantiate(bulletPrefab, endCharacter.position, Quaternion.identity);
        go.GetComponent<Bullet>().init(this, position);
        //cus.getDamage(towerInfo.attackDamage);
    }
    public override void activate()
    {
        base.activate();
        shoot(cardHappenCenter.position); 
        //Destroy(gameObject, 0.1f);
        ////do damage
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(cardHappenCenter.position, cardInfo.range, LayerMask.GetMask("Enemy"));
        //foreach(var enemyC in colliders)
        //{
        //    var enemy = enemyC.GetComponent<Customer>();
        //    if (enemy)
        //    {
        //        enemy.getDamage(cardInfo.damage,cardInfo);
        //    }
        //    else
        //    {
        //        Debug.LogError("customer not existed on " + enemy);
        //    }
        //}
        //Destroy(gameObject, 0.1f);
        //disappear about 1 sec
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
