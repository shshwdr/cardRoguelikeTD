using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelRequestCell : MonoBehaviour
{
    public TMP_Text descriptionLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(bool isPassingLevel , Dictionary<string,string> request)
    {
        descriptionLabel.text = "";
        if (isPassingLevel)
        {
            descriptionLabel.text = "过关条件：";
        }
        else
        {

            descriptionLabel.text = "额外奖励：";
        }
        var key = request.Keys.ToList()[0];
        var value = request.Values.ToList()[0];
        switch (key)
        {
            case "hasMoney":
                descriptionLabel.text += string.Format("持有现金{0}元", value);
                break;
            case "earnMoney":
                descriptionLabel.text += string.Format("收入现金{0}元", value);
                break;
            case "dontUse":
                descriptionLabel.text += string.Format("不要购买{0}", value);
                break;
            default:
                Debug.LogError("request not support " + key);
                break;


        }

    }

    public void init(KeyValuePair<string, string> request)
    {
        descriptionLabel.text = request.Key + ":" + request.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
