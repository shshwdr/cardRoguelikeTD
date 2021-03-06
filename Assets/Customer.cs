using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : HPCharacterController
{
    EmotesController emotes;
    public NPCPathFinding pathFinding;
    public Vector3 target;
    public Vector3 finalTarget;
    bool isBlocked = false;
    BlockTower blockTower;
    float blockTime = 0;
    float currentBlockTime = 0;
    HashSet<BlockTower> blockedTowers = new HashSet<BlockTower>();
    CustomerInfo customerInfo;
    bool canBlock = false;
    bool noMoney = false;

    public Animator animator;



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
        animator = GetComponentInChildren<Animator>();
        spriteObject = animator.gameObject;
        pathFinding = GetComponent<NPCPathFinding>();
        emotes = GetComponentInChildren<EmotesController>();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CustomerManager.Instance.addCustomer(this);

    }
    public void init(Vector3 _target, CustomerInfo info)
    {
        target = _target;
        finalTarget = target;
        customerInfo = info;
        maxHp = customerInfo.hp;
        hp = maxHp;
        GetComponent<NPCPathFinding>().moveSpeed = customerInfo.moveSpeed;
        canBlock = customerInfo.canBlock;


        pathFinding.setTarget(target);

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

        if (currentBuffs.ContainsKey(BuffType.Frost) && currentBuffs[BuffType.Frost].Count > 0)
        {
            var nextBuff = new List<Buff>();
            foreach (var buff in currentBuffs[BuffType.Frost])
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
            currentBuffs[BuffType.Frost] = nextBuff;
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

    public bool isConfused()
    {
        return currentBuffs.ContainsKey(BuffType.Confused) && currentBuffs[BuffType.Confused].Count > 0;
    }

    public override bool isFlying()
    {
        return customerInfo.isFlying == 1;
    }

    public void clean()
    {
        animator.SetTrigger("die");
        pathFinding.cancelPath();
        CustomerManager.Instance.removeCustomer(this);
        if(MouseManager.Instance.currentFocusTarget == GetComponent<SelectToFocusTarget>())
        {
            MouseManager.Instance.currentFocusTarget = null;
        }
        Destroy(gameObject,1);
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
