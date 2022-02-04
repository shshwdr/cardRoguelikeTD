using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyValuePairCell : MonoBehaviour
{
    public TMP_Text keyLabel;
    public TMP_Text valueLabel;

    public void init(string key, int value)
    {
        keyLabel.text = key;
        valueLabel.text = value.ToString();
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
