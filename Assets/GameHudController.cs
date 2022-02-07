using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHudController : MonoBehaviour
{
    public Text coinLabel;
    public Text lifeLabel;
    // Start is called before the first frame update
    void Start()
    {
        updateCoin();
        EventPool.OptIn("coinChanged", updateCoin);
        EventPool.OptIn("updateCharacterHP", updateCharacterHP);

    }
    void updateCharacterHP()
    {
        lifeLabel.text = BattleManager.Instance.characterLife().ToString();
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
