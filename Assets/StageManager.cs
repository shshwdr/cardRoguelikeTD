using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfo
{
    public string customer;
    public int customerLevel;
    public int customerCount;
    public float customerIntervalTime;
    public float waitTimeTillNextRound;


    public StageInfo (string[] input)
    {
        customer = input[0];
        customerLevel = int.Parse(input[1]);
        customerCount = int.Parse(input[2]);
        customerIntervalTime = float.Parse(input[3]);
        waitTimeTillNextRound = float.Parse(input[4]);
    }
}

public class StageManager : Singleton<StageManager>
{
    public Transform startingPoint;
    public Transform endPoint;


    int currentStageId = 0;
    int currentRoundId = 0;
    
    float nextRoundTime = 0f;
    int roundCustomerNumber = 0;
    int currentRoundCounter = 0;
    float nextEnemyTime = 0f;
    float currentTimer = 0;
    float currentRoundTimer = 0;

    List<StageInfo> currentStage;
    StageInfo currentRound;
    string currentCustomerName;


    bool isStageStarted = false;
    public Button nextRoundButton;
    public Image nextRoundImage;

    public bool isLoop = false;


    int pickCardEveryTurn = 3;
    int turnNum = 0;

    List<List<StageInfo>> stageInfos = new List<List<StageInfo>>();
    // Start is called before the first frame update
    void Awake()
    {
        var infos = CsvUtil.LoadList<string>("Stage",false);
        foreach(var info in infos)
        {
            var stageInfoRow = new List<StageInfo>();
            foreach(var oneInfo in info)
            {
                var spliteValues = oneInfo.Split('|');
                if (spliteValues.Length != 5)
                {
                    Debug.LogError("stage info cant split into 5 values " + oneInfo);
                }
                StageInfo stageInfo = new StageInfo(spliteValues);

                stageInfoRow.Add(stageInfo);
            }
            stageInfos.Add(stageInfoRow);
        }

        prepare();
    }

    void prepare()
    {
        turnNum++;
        if (turnNum >= pickCardEveryTurn)
        {
            turnNum = 0;
            GameObject.FindObjectOfType<PickCardController>(true).showView();
        }
        if (isStageFinished())
        {
            if (isLoop)
            {
                currentRoundId = 0;
            }
            else
            {
                return;
            }
        }

            currentStage = stageInfos[currentStageId];
        currentRound = currentStage[currentRoundId];
        roundCustomerNumber = currentRound.customerCount;
        nextRoundTime = currentRound.waitTimeTillNextRound;
        nextEnemyTime = currentRound.customerIntervalTime;
        currentCustomerName = currentRound.customer;
    }

    private void Start()
    {
        nextRoundButton.onClick.AddListener(delegate { clickNextRoundButton(); });
    }

    void clickNextRoundButton()
    {
        if (!isStageStarted)
        {
            isStageStarted = true;
            return;
        }

        currentRoundCounter = 0;
        currentTimer = 0;
        //currentRoundTimer = 0;

        currentRoundId++;

        prepare();
        updateNextRoundButton();

    }

    void updateNextRoundButton()
    {

        nextRoundButton.gameObject.SetActive(true);
        nextRoundImage.fillAmount = (nextRoundTime - currentTimer) / nextRoundTime;
    }

    void hideNextRoundButton()
    {
        nextRoundButton.gameObject.SetActive(false);
    }

    public bool isStageFinished()
    {
        //???
        if (currentStage == null)
        {
            return false;
        }
        return currentRoundId >= currentStage.Count;
    }
    void stageFinish()
    {
       // finishedStageUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isStageStarted)
        {
            return;
        }
        if (isStageFinished())
        {
            if (isLoop)
            {
                currentRoundId = 0;
            }
            else
            {
                hideNextRoundButton();
                return;

            }
        }
        //currentRoundTimer += Time.deltaTime;
        if (currentRoundCounter >= roundCustomerNumber)
        {

            updateNextRoundButton();
            currentTimer += Time.deltaTime;
            if (currentTimer >= nextRoundTime)
            {
                currentRoundCounter = 0;
                currentTimer = 0;
                //currentRoundTimer = 0;

                currentRoundId++;
                //if (isStageFinished())
                //{
                //    stageFinish();
                //}
                prepare();
            }
        }
        else
        {
            hideNextRoundButton();
            currentTimer += Time.deltaTime;
            if (currentTimer >= nextEnemyTime)
            {
                var enemyPrefab = Resources.Load<GameObject>("customer/"+ currentCustomerName);
                var go = Instantiate(enemyPrefab, startingPoint.position, Quaternion.identity);
                go.GetComponent<Customer>().init(endPoint.position, CustomerManager.Instance.CustomerInfoDict[currentCustomerName]);
               // go.GetComponent<Customer>().pathFinding.setTarget(go.GetComponent<Customer>().target);
                currentTimer = 0;
                currentRoundCounter++;
            }
        }
    }
}
