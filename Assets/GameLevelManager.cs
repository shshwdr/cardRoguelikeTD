using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameLevelInfo : BaseInfo {
    public int stage;
    public Dictionary<string, string> request;
    public Dictionary<string, string> extraRequests;

}

public class GameLevelManager : Singleton<GameLevelManager>
{

    public Dictionary<string, GameLevelInfo> GameLevelDict = new Dictionary<string, GameLevelInfo>();
    private void Awake()
    {
        var customerList = CsvUtil.LoadObjects<GameLevelInfo>("Level");
        foreach (var info in customerList)
        {
            GameLevelDict[info.name] = info;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
