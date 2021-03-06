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
    public Dictionary<Vector2Int, Transform> gridItemPathDict = new Dictionary<Vector2Int, Transform>();
    public Dictionary<Vector2Int, Transform> gridItemBuildDict = new Dictionary<Vector2Int, Transform>();
    public Dictionary<Vector2Int, GridOverlay> gridOverlayDict = new Dictionary<Vector2Int, GridOverlay>();
    private void Awake()
    {
        Utils.gridSize = gridSize;
        startPosition = Utils.snapToGridCenter(startPoint.position);
        startGridPos = Utils.positionToGridIndexCenter2d(startPosition);
        endPosition = Utils.snapToGridCenter(endPoint.position);
        var endGridPos = Utils.positionToGridIndexCenter2d(endPosition);

        width = (endGridPos - startGridPos).x+1 ;
        height = (endGridPos - startGridPos).y+1;


        var overlayPrefab = Resources.Load<GameObject>("overlay");
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j) + startGridPos;
                gridItemPathDict[pos] = null;
                gridItemBuildDict[pos] = null;
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

    public bool checkIfPlaceCanBeOccupied(DraggableItem item)
    {

        var gridPos = Utils.positionToGridIndexCenter2d(item.transform.position);
        if (gridItemBuildDict.ContainsKey(gridPos) && gridItemBuildDict[gridPos]==null)
        {
            if (PathFindingManager.Instance.testIfCanBeOccupied(item))
            {
                return true;
            }
        }
        return false;
    }

    public void addItem(Transform item, bool isPathChanging)
    {
        var gridPos = Utils.positionToGridIndexCenter2d(item.transform.position);
        //item.gridPos = gridPos;
        gridItemBuildDict[gridPos] = item;
        if (isPathChanging)
        {

            gridItemPathDict[gridPos] = item;
        }
        PathFindingManager.Instance.clearCanBeOccupied();
        //gridOverlayDict[gridPos].updateColor(Color.red);
        //EventPool.Trigger("regenerateNav");
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
