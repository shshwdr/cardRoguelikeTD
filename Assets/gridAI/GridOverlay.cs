using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOverlay : MonoBehaviour,IClickable
{
    float transparency = 0.5f;
    SpriteRenderer renderer;

    public GameObject selections;

    public SelectableTowerCell[] selectableCells;

    public string type;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        selectableCells = selections.GetComponentsInChildren<SelectableTowerCell>();
    }

    public void showInfo()
    {
        selections.SetActive(true);
        renderer.color = new Color(1, 1, 1,0.5f);

        int i = 0;
        foreach(var info in TowerManager.Instance.towerDict.Values)
        {
            if(info.placeType == type && info.isUnlocked == 1)
            {
                if (i >= selectableCells.Length)
                {
                    Debug.LogError("too many items but not enough cells, plan to add item "+i.ToString()+" but only have cell count "+ selectableCells.Length.ToString());
                }
                selectableCells[i].gameObject.SetActive(true);
                selectableCells[i].GetComponent<SelectableTowerCell>().init(info,this);
                i++;
            }
        }
        for (; i < selectableCells.Length; i++)
        {
            selectableCells[i].gameObject.SetActive(false);

        }
        

    }
    public void hideInfo()
    {
        selections.SetActive(false);


        renderer.color = new Color(0, 0, 0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateColor(Color color)
    {
       // renderer.color = new Color(color.r, color.g, color.b, transparency);
    }
}
