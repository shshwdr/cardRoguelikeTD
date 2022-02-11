using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardDetail : MonoBehaviour
{
    public Text nameLabel;
    public Text descriptionLabel;
    public void init(CardInfo card)
    {
        nameLabel.text = card.displayName;
        descriptionLabel.text = card.description;
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
