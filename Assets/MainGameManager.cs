using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : Singleton<MainGameManager>
{
    public string currentLevelName = "introduction";
    public GameLevelInfo currentLevelInfo;
    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }
    void startGame()
    {
        currentLevelInfo = GameLevelManager.Instance.GameLevelDict[currentLevelName];


        EventPool.Trigger("levelChanged");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
