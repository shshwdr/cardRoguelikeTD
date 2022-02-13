using Pool;
using Priority_Queue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PathFindingNode {
    public Vector2Int gridPos;
    public int distance;
    public PathFindingNode(Vector2Int p, int d)
    {
        gridPos = p;
        distance = d;
    }
}

public class PathFindingManager : Singleton<PathFindingManager>
{
    public Transform overlayParent;
    public Transform startPoint;
    public Transform endPoint;
    Vector3 startPosition;
    public Vector2Int startGridPos;
    Vector3 endPosition;
    public Vector2Int endGridPos;
    // Start is called before the first frame update
    void Start()
    {

        //regenerateNav();
       // EventPool.OptIn("regenerateNav", regenerateNav);

    }
    public Dictionary<Vector2Int, bool> canBeOccupied = new Dictionary<Vector2Int, bool>();

    public bool testIfCanBeOccupied(DraggableItem item)
    {

        var ask = Utils.positionToGridIndexCenter2d(item.transform.position);
        if (canBeOccupied.ContainsKey(ask))
        {
            return canBeOccupied[ask];
        }
        var grid = new Dictionary<Vector2Int, Transform>( GridManager.Instance.gridItemPathDict);
        if (item.isPathChanging)
        {

            grid[ask] = transform;
        }
        var res = canFindPath(grid);
        canBeOccupied[ask] = res;
        return res;
    }

    public void clearCanBeOccupied()
    {
        canBeOccupied.Clear();
    }

    //todo make it static
    public bool canFindPath(Dictionary<Vector2Int, Transform> gridItemDict)
    {
        startPosition = Utils.snapToGrid(startPoint.position);
        startGridPos = Utils.positionToGridIndex2d(startPosition);
        endPosition = Utils.snapToGrid(endPoint.position);
        endGridPos = Utils.positionToGridIndex2d(endPosition);

        SimplePriorityQueue<PathFindingNode> priorityQueue = new SimplePriorityQueue<PathFindingNode>();

        Dictionary<Vector2Int, Vector2Int> posToPreviousPos = new Dictionary<Vector2Int, Vector2Int>();

        priorityQueue.Enqueue(new PathFindingNode(startGridPos, 0), 0);
        posToPreviousPos[startGridPos] = startGridPos;
        if ((gridItemDict.ContainsKey(startGridPos) && gridItemDict[startGridPos] != null) || (gridItemDict.ContainsKey(endGridPos) && gridItemDict[endGridPos] != null))
        {
            return false;
        }
            //var gridItemDict = GridManager.Instance.gridItemDict;
            int loopBreaker = 100000;
        while (priorityQueue.Count > 0)
        {
            if (loopBreaker == 0)
            {
                break;
            }
            loopBreaker--;
            var deque = priorityQueue.Dequeue();
            int currentDistance = deque.distance;
            Vector2Int currentPos = deque.gridPos;
            int newDistance = currentDistance + 1;
            foreach (var dir in Utils.dir4V2Int)
            {
                Vector2Int newPos = currentPos + dir;
                if (gridItemDict.ContainsKey(newPos) && gridItemDict[newPos] != null)
                {
                    newPos = currentPos + dir;
                }
                if (gridItemDict.ContainsKey(newPos) && gridItemDict[newPos] == null && !posToPreviousPos.ContainsKey(newPos))
                {
                    int newPriority = currentDistance + Utils.ManhattanDistance(newPos, endGridPos);

                    posToPreviousPos[newPos] = currentPos;

                    if (newPos == endGridPos)
                    {
                        break;
                    }

                    priorityQueue.Enqueue(new PathFindingNode(newPos, newDistance), newPriority);
                }
            }
            if (posToPreviousPos.ContainsKey(endGridPos))
            {
                break;
            }

        }
        print("finish finding road");
        if (posToPreviousPos.ContainsKey(endGridPos))
        {

            Vector2Int currentPos = endGridPos;
            while (true)
            {
                if (loopBreaker == 0)
                {
                    break;
                }
                loopBreaker--;
                //GridManager.Instance.gridOverlayDict[currentPos].updateColor(Color.green);
                Vector2Int previousPos = posToPreviousPos[currentPos];
                if (previousPos == currentPos)
                {
                    break;
                }
                currentPos = previousPos;
            }
            print("print road");
            return true;
        }
        else
        {
            print("no valid road");
            return false;
        }
    }
    //public void toggleShowOverlay()
    //{
    //    overlayParent.gameObject.SetActive(!overlayParent.gameObject.active);
    //}
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    toggleShowOverlay();
        //}
    }
}
