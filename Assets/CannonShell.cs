using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShell : Bullet
{
    Vector3 startPosition;
    float startTime;

    float totalTime;
    public float height;
    Vector3 lastPosition;

    public override void init(Tower tower, Transform t)
    {
        base.init(tower, t);
        startTime = Time.time;
        totalTime = 1 / speed;
        startPosition = transform.position;
    }

    public override void init(AreaDamageSpell spell, Vector3 position)
    {
        base.init(spell, position);
        startTime = Time.time;
        totalTime = 1 / speed;
        startPosition = transform.position;
    }

    void Update()
    {
        if (!hasHit)
        {

            var time = (Time.time - startTime) / totalTime;
            transform.position = Utils.Parabola(startPosition, targetPosition, height, time);
            transform.right = transform.position - lastPosition;
            lastPosition = transform.position;
            if (time >= 1)
            {
                hit();
            }
        }
    }
}
