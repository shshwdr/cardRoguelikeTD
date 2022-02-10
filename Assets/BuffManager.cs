using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    public Dictionary<string, BuffInfo> BuffInfoDict = new Dictionary<string, BuffInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        var customerList = CsvUtil.LoadObjects<BuffInfo>("Buff");
        foreach (var info in customerList)
        {
            BuffInfoDict[info.name] = info;
        }
    }

    public BuffInfo getBuffInfo(string n)
    {
        if (!BuffInfoDict.ContainsKey(n))
        {
            Debug.LogError("buff not exists " + n);
        }
        return BuffInfoDict[n];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
