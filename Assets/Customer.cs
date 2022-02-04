using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : HPCharacterController
{
    EmotesController emotes;
    NPCPathFinding pathFinding;
    public Transform target;
    bool isBlocked = false;
    BlockTower blockTower;
    float blockTime = 0;
    float currentBlockTime = 0;
    HashSet<BlockTower> blockedTowers = new HashSet<BlockTower>();
    CustomerInfo customerInfo;
    bool canBlock = false;
    bool noMoney = false;

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
    }

    public void spentAllMoney()
    {
        noMoney = true;
        //CustomerManager.Instance.removeCustomer(this);
        emotes.showNoMoney();
        Debug.Log("no money");
        if (isBlocked)
        {

            getReleased();
        }

        pathFinding.moveSpeed *= 2;
    }

    public override void Die()
    {
        spentAllMoney();
    }

    public void finishedShopping()
    {

        base.Die();
        CustomerManager.Instance.removeCustomer(this);
        Destroy(gameObject);
    }

    public void spendMoney()
    {
        Inventory.Instance.addCoin((int)(maxHp - hp));
    }
}
