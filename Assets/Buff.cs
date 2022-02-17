using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType { Frost,Confused};


public class BuffInfo : BaseInfo
{
    public float duration;
    public float effect;
    

}

public class Buff {
    BuffInfo info;
    public float invalidTime;
    public int level;

    public Buff(BuffInfo inf,int lev)
    {
        info = inf;
        invalidTime = inf.duration + Time.time;
        level = lev;
    }

    public float effect { get { return info.effect; } }
}
