using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PickableCard  : MonoBehaviour
{
    string cardName;
    public void init(string n)
    {
        cardName = n;

        GetComponent<Button>().onClick.AddListener(delegate {
            DeckManager.Instance.addCardToDeck(cardName);
            transform.parent. GetComponentInParent<PickCardController>(). hideView();
        });
    }
}