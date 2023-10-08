using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCharacterController : MonoBehaviour
{
    protected Animator animator;
    public float maxHp = 10;
    protected float hp = 0;
    HPBarHandler hpBar;
    public bool isDead;
    public bool isStuned;

    public float speedAdjust;

    public AudioClip[] beHitClips;

    public Rigidbody2D rb;

    public float stunTime = 0.3f;
    float currentStunTimer = 0;

    public bool hasInvinsibleTime;
    public float invinsibleTime = 0.3f;
    float currentInvinsibleTimer;
    //protected EmotesController emotesController;
    protected GameObject spriteObject;

    public virtual bool isFlying()
    {
        return false;
    }
    // Start is called before the first frame update
    virtual protected void Awake()
    {

        //emotesController = GetComponentInChildren<EmotesController>();
        hpBar = GetComponentInChildren<HPBarHandler>();
        rb = GetComponent<Rigidbody2D>();
    }
    virtual protected void Start()
    {
        hp = maxHp;
        hpBar.SetMaxHp((int)maxHp);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (isStuned)
        {
            currentStunTimer += Time.deltaTime;
            if (currentStunTimer >= stunTime)
            {
                isStuned = false;
            }
        }
        currentInvinsibleTimer += Time.deltaTime;

        
    }

    public void updateHP()
    {

        hp = Mathf.Clamp(hp, 0, maxHp);
        hpBar.SetHealthBarValue(hp / (float)(maxHp));
    }
    virtual protected void playHurtSound()
    {

    }

    protected Dictionary<BuffType, List<Buff>> currentBuffs = new Dictionary<BuffType, List<Buff>>();

    // public void applyBuff(Dictionary<string,int> buffs,Tower applyee = null)
    // {
    //     foreach(var pair in buffs)
    //     {
    //         BuffInfo buffInfo = BuffManager.Instance.getBuffInfo(pair.Key);
    //         Buff buff = new Buff(buffInfo, pair.Value);
    //         if (applyee != null)
    //         {
    //             buff.tower = applyee;
    //         }
    //         if (!currentBuffs.ContainsKey(BuffManager.Instance.buffStringToType[ buffInfo.name]))
    //         {
    //             currentBuffs[BuffManager.Instance.buffStringToType[buffInfo.name]] = new List<Buff>();
    //         }
    //         currentBuffs[BuffManager.Instance.buffStringToType[buffInfo.name]].Add(buff);
    //     }
    // }
    


    public bool canBeDamaged(Tower tower)
    {
        if ((isFlying() && !tower.canAttackFlying) || (isFlying() && !tower.canAttackGround))
        {
            return false;
        }

        if (isDead)
        {
            return false;
        }
        return true;
    }

    public void getDamage(float damage, CardInfo card)
    {
        getDamage(damage);
    }

    public void getDamage(float damage)
    {
        if (isDead)
        {
            return;
        }
        if (hasInvinsibleTime && currentInvinsibleTimer < invinsibleTime)
        {
            return;
        }

        currentInvinsibleTimer = 0;
        hp -= damage;
        //playHurtSound();
        updateHP();
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            isStuned = true;
            currentStunTimer = 0;
            //animator.SetTrigger("hit");
        }
    }

    public void getDamage(float damage,Tower tower)
    {
        if ((isFlying() && !tower.canAttackFlying) || (isFlying() && !tower.canAttackGround))
        {
            return;
        }
        getDamage(damage);
        

    }

    public virtual void Die()
    {
        isDead = true;
    }


    bool facingRight = true;
    void flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = spriteObject.transform.localScale;
        scaler.x = -scaler.x;
        // spriteObject.transform.position = new Vector3(spriteObject.transform.position.x + 1, spriteObject.transform.position.y, -1);
        spriteObject.transform.localScale = scaler;
        //spriteObject.GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
    public void testFlip(Vector3 movement)
    {
        if (facingRight == false && movement.x > 0f)
        {
            flip();
        }
        if (facingRight == true && movement.x < 0f)
        {
            flip();
        }
    }
}
