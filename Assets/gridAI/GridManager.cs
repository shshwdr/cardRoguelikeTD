using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridManager : Singleton<GridManager>
{
    public Transform overlayParent;
    public Transform startPoint;
    public Transform endPoint;
    Vector3 startPosition;
    public Vector2Int startGridPos;
    Vector3 endPosition;
    float gridSize = 0.32f;
    
    public int width;
    public int height;
    public Dictionary<Vector2Int, GridItem> gridItemDict = new Dictionary<Vector2Int, GridItem>();
    public Dictionary<Vector2Int, GridOverlay> gridOverlayDict = new Dictionary<Vector2Int, GridOverlay>();
    private void Awake()
    {
        Utils.gridSize = gridSize;
        startPosition = Utils.snapToGrid(startPoint.position);
        startGridPos = Utils.positionToGridIndex2d(startPosition);
        endPosition = Utils.snapToGrid(endPoint.position);

        width = (int)((endPosition - startPosition).x / gridSize);
        height = (int)((endPosition - startPosition).y / gridSize);


        var overlayPrefab = Resources.Load<GameObject>("overlay");
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j) + startGridPos;
                gridItemDict[pos] = null;
                gridOverlayDict[pos] = Instantiate(overlayPrefab, new Vector3(i * gridSize, j * gridSize, 0)+startPosition, Quaternion.identity, overlayParent).GetComponent<GridOverlay>();

            }
        }
    }

    public void toggleShowOverlay()
    {
        overlayParent.gameObject.SetActive(!overlayParent.gameObject.active);
    }
    
    public void updateOverlay()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j) + startGridPos;
                gridOverlayDict[pos].GetComponent<GridOverlay>().updateColor(Color.white);

            }
        }
    }

    public void addItem(GridItem item)
    {
        var gridPos = Utils.positionToGridIndex2d(item.transform.position);
        item.gridPos = gridPos;
        gridItemDict[gridPos] = item;
        gridOverlayDict[gridPos].updateColor(Color.red);
        EventPool.Trigger("regenerateNav");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            toggleShowOverlay();
        }
    }
}
