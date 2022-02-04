using UnityEngine;
using UnityEngine.UI;
using Pool;

public class MouseManager : Singleton<MouseManager>
{
    Vector3 screenPoint;
    Camera dragCamera;
    public GameObject currentDragItem;
    public bool isInBuildMode;
    public GameObject selectedItem;
    public float rotateSmooth = 10.0f;

    public IClickable previousSelected;
    //void selectItem(GameObject go)
    //{
    //    selectedItem = go;
    //}
    //public void deselectIem()
    //{
    //    selectedItem = null;
    //}
    //public void startDragItem(GameObject go)
    //{
    //    if (currentDragItem)
    //    {
    //        Debug.Log("already have dragItem");
    //        Destroy(currentDragItem);
    //    }
    //    currentDragItem = go;
    //}
    //public void cancelCurrentDragItem()
    //{
    //    if (currentDragItem)
    //    {
    //        currentDragItem.GetComponent<Draggable>().removeDragItem();
    //    }
    //    isInBuildMode = false;
    //}

    //public void finishCurrentDragItem()
    //{
        

    //    isInBuildMode = false;
    //    currentDragItem = null;
    //}

    //public void startBuildMode()
    //{
    //    isInBuildMode = true;
    //}
    //public void cancelDragItem(GameObject go)
    //{
    //    if(currentDragItem != go)
    //    {
    //        Debug.LogError("cancel " + go + " is not the current one " + currentDragItem);
    //    }

    //    Destroy(currentDragItem);
    //    currentDragItem = null;
    //}

    //public void finishDragItem(GameObject go)
    //{

    //    if (currentDragItem != go)
    //    {
    //        Debug.LogError("cancel " + go + " is not the current one " + currentDragItem);
    //    }

    //    currentDragItem = null;
    //}
    private void Start()
    {
        dragCamera = Camera.main;
        //Doozy.Engine.GameEventMessage.SendEvent("addItem");
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                //print("on ui");
                return;
            }
            if (previousSelected!=null)
            {
                previousSelected.hideInfo();
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
            if (hits.Length>0)
            {
                foreach (var hit in hits)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    var gridOverlay = hit.collider.GetComponent<Tower>();
                    if (gridOverlay)
                    {
                        gridOverlay.showInfo();
                        previousSelected = gridOverlay;
                        return;
                    }
                }

                foreach (var hit in hits)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    var gridOverlay = hit.collider.GetComponent<GridOverlay>();
                    if (gridOverlay)
                    {
                        gridOverlay.showInfo();
                        previousSelected = gridOverlay;
                        return;
                    }
                }
            }
        }
        //if (currentDragItem)
        //{
        //    screenPoint = dragCamera.WorldToScreenPoint(currentDragItem.transform.position);

        //    float tilt = Input.GetAxisRaw("Rotate");
        //    currentDragItem.transform.RotateAround(Vector3.zero, Vector3.up* tilt, rotateSmooth * Time.deltaTime);

        //    var currentDraggable = currentDragItem.GetComponent<Draggable>();
        //    //if (isDragging)
        //    {
        //        bool canbuild = currentDraggable. canBuildItem();
        //        if (!canbuild)
        //        {
        //            currentDraggable.showDisableOverlay();
        //        }
        //        else
        //        {
        //            currentDraggable.showEnableOverlay();
        //        }

        //        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //        Vector3 mousePosition = dragCamera.ScreenToWorldPoint(newPosition);
        //        currentDraggable.transform.position = mousePosition;


        //    }


        //}




        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //    {
        //        print("on ui");
        //        return;
        //    }
        //    Debug.Log("mouse down");

        //    if (currentDragItem)
        //    {
        //        currentDragItem.GetComponent<Draggable>().removeDragItem();
        //        return;
        //    }

        //    LayerMask mask = LayerMask.GetMask("item");
        //    if (currentDragItem == null && isInBuildMode )
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //        foreach (var hit in Physics.RaycastAll(ray,1000, mask))
        //        {
        //            Debug.Log("hit " + hit.transform.gameObject);
        //            var hitItem = hit.transform.GetComponent<DraggableItem>();
        //            if (hitItem)
        //            {
        //                Doozy.Engine.GameEventMessage.SendEvent("ItemAction");
        //                selectItem(hit.transform.gameObject);
        //                return;
        //            }
        //        }
        //    }
        //    return;
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        //    {
        //        print("on ui");
        //        return;
        //    }

        //    if(selectedItem && currentDragItem)
        //    {
        //        Debug.LogError("could only hold one item");
        //        return;
        //    }
        //    if (currentDragItem)
        //    {
        //        currentDragItem.GetComponent<Draggable>().tryBuild();
        //    }

        //    if (selectedItem)
        //    {
        //        deselectIem();

        //        Doozy.Engine.GameEventMessage.SendEvent("CancelItemAction");
        //    }
        //    //if (currentDragItem == null)
        //    //{
        //    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    //    foreach (var hit in Physics.RaycastAll(ray))
        //    //    {
        //    //        Debug.Log("hit " + hit.transform.gameObject);
        //    //        var hitItem = hit.transform.GetComponent<DraggableItem>();
        //    //        if (hitItem)
        //    //        {
        //    //            if (hitItem.room.isEditing)
        //    //            {
        //    //                startDragItem(hitItem.gameObject);
        //    //                return;
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    return;
        //}
    }

    //public void removeSelectedRoom()
    //{
    //    if(!selectedItem || !selectedItem.GetComponent<DraggableRoom>())
    //    {
    //        Debug.LogError(selectedItem + " is not a room that can be removed");
    //    }
    //    selectedItem.GetComponent<DraggableRoom>().cancelDragItem();
    //}

    //public void startDraggingRoom()
    //{
    //    if (!selectedItem || !selectedItem.GetComponent<DraggableRoom>())
    //    {
    //        Debug.LogError(selectedItem + " is not a room that can be moved");
    //    }
    //    selectedItem.GetComponent<DraggableRoom>().getIntoEditMode();
    //}

    //public void editSelectedRoomItems()
    //{
    //    if (!selectedItem || !selectedItem.GetComponent<DraggableRoom>())
    //    {
    //        Debug.LogError(selectedItem + " is not a room that can be moved");
    //    }
    //    selectedItem.GetComponent<DraggableRoom>().getIntoEditMode();
    //}

    //public void removeCurrentSelect()
    //{
    //    //pop up 
    //    selectedItem.GetComponent<Draggable>().removeDragItem();
    //}
    //public void moveCurrentSelect()
    //{
    //    currentDragItem = selectedItem;
    //    currentDragItem.GetComponent<Draggable>().isDragging = true;
    //    selectedItem = null;
    //}
    //public void cancelSelect()
    //{
    //    selectedItem = null;
    //}
}