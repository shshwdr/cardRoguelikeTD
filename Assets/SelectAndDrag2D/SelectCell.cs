using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCell : MonoBehaviour
{
    public TMP_Text nameText;
    public Image image;
    bool previousPlantableState = false;


    public GameObject explainPanel;
    public TMP_Text explainText;

    string itemName;
    InfoBase itemInfo;
    protected GameObject prefab;

    BuildItemButton button;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void init(InfoBase info)
    {
        button = GetComponent<BuildItemButton>();
        itemName = info.name;
        itemInfo = info;
        //spawnPlantPrefab = plant;
        //helperPlant = plant.GetComponent<HelperPlant>();
        nameText.text = info.displayName;
        image.sprite = Resources.Load<Sprite>("icon/" + button.itemType() + "/" + itemName);
        //image.color = plant.GetComponent<SpriteRenderer>().color;
        
    }

    public void OnMouseDown()
    {
        if (button.CanPurchaseItem())
        {
            StartCoroutine(button.delaySpawn());
        }
    }
    public void PointerEnter()
    {
        explainPanel.SetActive(true);
        explainText.text = itemInfo.description;
    }
    public void PointerExit()
    {
        explainPanel.SetActive(false);
    }
}
