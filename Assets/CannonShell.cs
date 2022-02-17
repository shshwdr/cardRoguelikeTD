using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShell : Bullet
{
    Vector3 startPosition;
    float startTime;

    float totalTime;
    public float height;

    public override void init(Tower tower, Transform t)
    {
        base.init(tower, t);
        startTime = Time.time;
        totalTime = 1 / speed;
        startPosition = transform.position;
    }

    void Update()
    {
        var time = (Time.time - startTime)/totalTime;
        transform.position = Utils.Parabola(startPosition, targetPosition, height, time);
        if (time >= 1)
        {
            hit();
        }
    }
}
