using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHudController : MonoBehaviour
{
    public TMP_Text coinLabel;
    // Start is called before the first frame update
    void Start()
    {
        updateCoin();
        EventPool.OptIn("coinChanged", updateCoin);

    }

    void updateCoin()
    {
        coinLabel.text = Inventory.Instance.getCoin().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
