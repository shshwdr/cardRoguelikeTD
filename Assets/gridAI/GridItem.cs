using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public int occupyHeight = 1;
    public int occupyWidth = 1;
    public Vector2Int gridPos;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    void init()
    {
        Utils.snapToGrid(transform.position);
        GridManager.Instance.addItem(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
