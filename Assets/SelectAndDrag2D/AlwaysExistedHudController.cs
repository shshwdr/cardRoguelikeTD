using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlwaysExistedHudController : MonoBehaviour
{
    public TMP_Text levelLabel;
    public TMP_Text happinessLabel;
    public TMP_Text coinLabel;

    public void updateLabels()
    {
        levelLabel.text = BathhouseLevelManager.Instance.getLevel();
        happinessLabel.text = BathhouseLevelManager.Instance.getHappy().ToString();
        coinLabel.text = Inventory.Instance.itemAmount("coin").ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        updateLabels();
        EventPool.OptIn("happinessChanged", updateLabels);
        EventPool.OptIn("inventoryChanged", updateLabels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
