using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform target;
    public float damage;
    public float speed = 3f;
    public float hitDistance = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void init(float d, Transform t)
    {
        damage = d;
        target = t;
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        var dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed*Time.deltaTime);
        if((target.position - transform.position).sqrMagnitude <= hitDistance)
        {
            hit();
        }
    }

    void hit()
    {
        var hpObject = target.GetComponent<HPCharacterController>();
        if (hpObject)
        {
            hpObject.getDamage(damage);
        }
        Destroy(gameObject);
    }
}
