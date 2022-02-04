using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelRequestsController : MonoBehaviour
{
    public GameObject levelPanel;
    public TMP_Text titleLable;
    public LevelRequestCell[] levelRequestCells;
    private void Awake()
    {
        levelRequestCells = GetComponentsInChildren<LevelRequestCell>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("showLevelInfo", 0.1f);
    }

    public void showLevelInfo()
    {
        var info = MainGameManager.Instance.currentLevelInfo;
        levelPanel.SetActive(true);
        titleLable.text = info.displayName;
        levelRequestCells[0].init(true,info.request);
        int i = 1;
        //foreach(var extraRequest in info.extraRequests)
        //{
        //    levelRequestCells[i].init(false,extraRequest);
        //    i++;
        //}
    }

    public void hideLevelInfo()
    {

        levelPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
