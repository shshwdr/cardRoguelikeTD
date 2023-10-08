using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PickCardController : MonoBehaviour
{
    public Button skip;
    public int selectNum = 3;
    PickableCard[] cardCells;
    public void hideView()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void showView()
    {

        gameObject.SetActive(true);
        Time.timeScale = 0;

        var candidateCards = Utils.GetRandomItemsFromList(CardManager.Instance.cardInfoDict.Where(x=>x.Value.isUnlocked == 1).Select(x=>x.Key).ToList(), selectNum);
        int i = 0;
        for (;i< candidateCards.Count; i++)
        {
            cardCells[i].gameObject.SetActive(true);
            cardCells[i].GetComponent<HandCardDetail>().init(CardManager.Instance.getCardInfo(candidateCards[i]));
            cardCells[i].GetComponent<PickableCard>().init(candidateCards[i]);
            
        }

        for(;i< cardCells.Length; i++)
        {
            cardCells[i].gameObject.SetActive(false);

        }

    }
    private void Awake()
    {

        cardCells = GetComponentsInChildren<PickableCard>(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        skip.onClick.AddListener( delegate {  hideView(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
