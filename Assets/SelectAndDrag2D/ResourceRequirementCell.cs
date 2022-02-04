using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceRequirementCell : MonoBehaviour
{
    public TMP_Text nameLabel;
    public TMP_Text requireLabel;
    public TMP_Text existedLabel;

    public void Init(KeyValueBase info)
    {
        Init(info.key, info.amount);
    }
    public void Init(string n,int requireAmount)
    {
        nameLabel.text = n;
        requireLabel.text = requireAmount.ToString();
        int existedAmount = Inventory.Instance.itemAmount(n);
        existedLabel.text = existedAmount.ToString();

        if (requireAmount > existedAmount)
        {
            requireLabel.color = Color.red;
        }
        else
        {
            requireLabel.color = Color.black;
        }
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
