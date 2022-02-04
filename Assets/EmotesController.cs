using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotesController : MonoBehaviour
{
    public GameObject noMoneyObject;
    public void showNoMoney()
    {
        noMoneyObject.SetActive(true);
    }
    public void hideNoMoney()
    {

        noMoneyObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        hideNoMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
