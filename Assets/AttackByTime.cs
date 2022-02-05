
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackByTime : MonoBehaviour
{
    public Transform shootPoint;

    float currentCoolDown = 0;

    public GameObject bulletPrefab;
    Tower tower;

    public TowerInfo towerInfo { get {
            if (!tower)
            {
                Debug.Log("no tower yet!");
            }
            return tower.towerInfo; } }


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
    private void Awake()
    {
        tower = GetComponent<Tower>();
        shootPoint = transform.Find("shootPoint");
    }

    private void Update()
    {
        if (!GetComponent<CardEffect>().isActivated)
        {
            return;
        }
        currentCoolDown += Time.deltaTime;
        if (currentCoolDown >= tower.towerInfo.attackTime)
        {
            currentCoolDown = 0;
            attack();
        }
    }


    protected virtual void attack()
    {

    }
}