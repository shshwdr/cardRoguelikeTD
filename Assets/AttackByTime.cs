
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackByTime : MonoBehaviour
{
    public Transform shootPoint;

    float currentCoolDown = 0;

    protected Tower tower;

    //public TowerInfo towerInfo { get {
    //        if (!tower)
    //        {
    //            Debug.Log("no tower yet!");
    //        }
    //        return tower.towerInfo; } }


    public GameObject renderObject
    {
        get
        {
            if (!tower)
            {
                Debug.Log("no tower yet!");
            }
            return tower.renderObject;
        }
    }
    protected virtual void Awake()
    {
        tower = GetComponent<Tower>();
        shootPoint = transform.Find("shootPoint");
    }
    protected virtual void Start()
    {
    }

    private void Update()
    {
        if (!GetComponent<CardEffect>().isActivated)
        {
            return;
        }
        currentCoolDown += Time.deltaTime;
        if (currentCoolDown >= tower.attackTime)
        {
            currentCoolDown = 0;
            attack();
        }
    }


    protected virtual void attack()
    {

    }
}