using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : HPCharacterController
{
    EmotesController emotes;
    NPCPathFinding pathFinding;
    public Transform target;
    public Transform finalTarget;
    bool isBlocked = false;
    BlockTower blockTower;
    float blockTime = 0;
    float currentBlockTime = 0;
    HashSet<BlockTower> blockedTowers = new HashSet<BlockTower>();
    CustomerInfo customerInfo;
    bool canBlock = false;
    bool noMoney = false;
    Seeker seeker;

    public bool getBlocked(BlockTower tower)
    {
        if (blockedTowers.Contains(tower))
        {
            return false;
        }
        if (isDead)
        {
            return false;
        }
        if (!canBlock)
        {
            return false;
        }
        if (noMoney)
        {
            return false;
        }
        isBlocked = true;
        blockTower = tower;
        blockTime = blockTower.getBlockTime();
        blockedTowers.Add(blockTower);
        pathFinding.cancelPath();
        return true;
    }

    public void getReleased()
    {
        currentBlockTime = 0;
        isBlocked = false;
        blockTower.releaseCustomer(this);
        blockTower = null;
        pathFinding.setTarget(target);
    }


    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
        spriteObject = gameObject;
        pathFinding = GetComponent<NPCPathFinding>();
        emotes = GetComponentInChildren<EmotesController>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CustomerManager.Instance.addCustomer(this);

        pathFinding.setTarget(target);
    }
    public void init(Transform _target, CustomerInfo info)
    {
        target = _target;
        finalTarget = target;
        customerInfo = info;

        hp = customerInfo.hp;
        GetComponent<NPCPathFinding>().moveSpeed = customerInfo.moveSpeed;
        canBlock = customerInfo.canBlock;



    }

    protected override void Update()
    {
        base.Update();
        if (isBlocked)
        {

            currentBlockTime += Time.deltaTime;
            if (currentBlockTime >= blockTime)
            {
                getReleased();
            }
        }

        if (currentBuffs.ContainsKey("Frost") && currentBuffs["Frost"].Count > 0)
        {
            var nextBuff = new List<Buff>();
            foreach (var buff in currentBuffs["Frost"])
            {
                speedAdjust = 1 - buff.effect;
                if (buff.invalidTime <= Time.time)
                {

                }
                else
                {
                    nextBuff.Add(buff);
                }

            }
            currentBuffs["Frost"] = nextBuff;
            spriteObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            //reset speed
            speedAdjust = 1;
            spriteObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        GetComponent<NPCPathFinding>().moveSpeed = customerInfo.moveSpeed * speedAdjust;
    }

    public void clean()
    {

        CustomerManager.Instance.removeCustomer(this);
        if(MouseManager.Instance.currentFocusTarget == GetComponent<SelectToFocusTarget>())
        {
            MouseManager.Instance.currentFocusTarget = null;
        }
        Destroy(gameObject);
    }

    public override void Die()
    {
        base.Die();

        foreach(var behavior in GetComponents<BehaviorWhenDestroyed>())
        {
            behavior.onDestroyed(null);
        }

        //add coins
        Inventory.Instance.addCoin(customerInfo.reward);
        clean();


    }

    public void finishedShopping()
    {
        //do damage to player
        BattleManager.Instance.getDamage(1);
        clean();
    }

    //public void spendMoney()
    //{
    //    Inventory.Instance.addCoin((int)(maxHp - hp));
    //}
}
