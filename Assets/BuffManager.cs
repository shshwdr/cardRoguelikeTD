using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{

    public Dictionary<string, BuffType> buffStringToType = new Dictionary<string, BuffType>();
    public Dictionary<BuffType, string> buffTypeToString = new Dictionary<BuffType, string>(){
    {BuffType.Frost,"Frost" },
    {BuffType.Confused,"Confused" },
};
    public Dictionary<string, BuffInfo> BuffInfoDict = new Dictionary<string, BuffInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach(var pair in buffTypeToString)
        {
            buffStringToType[pair.Value] = pair.Key;
        }

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
