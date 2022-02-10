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

    public TowerInfoController currentSelectedInfo;

    public SelectToFocusTarget currentFocusTarget;
    public LayerMask focusTargetLayer;

    public Tower currentHoverTower;
    void selectItem(GameObject go)
    {
        selectedItem = go;
    }
    public void deselectIem()
    {
        selectedItem = null;
    }
    public void startDragItem(GameObject go)
    {
        if (currentDragItem)
        {
            Debug.Log("already have dragItem");
            Destroy(currentDragItem);
        }
        currentDragItem = go;
    }
    public void cancelCurrentDragItem()
    {
        if (currentDragItem)
        {
            currentDragItem.GetComponent<Draggable>().removeDragItem();
        }
        isInBuildMode = false;
    }

    public void finishCurrentDragItem()
    {


        isInBuildMode = false;
        currentDragItem = null;
    }

    public void startBuildMode()
    {
        isInBuildMode = true;
    }
    public void cancelDragItem(GameObject go)
    {
        if (currentDragItem != go)
        {
            Debug.LogError("cancel " + go + " is not the current one " + currentDragItem);
        }

        Destroy(currentDragItem);
        currentDragItem = null;
    }

    public void finishDragItem(GameObject go)
    {

        if (currentDragItem != go)
        {
            Debug.LogError("cancel " + go + " is not the current one " + currentDragItem);
        }

        currentDragItem = null;
    }
    private void Start()
    {
        dragCamera = Camera.main;
        //Doozy.Engine.GameEventMessage.SendEvent("addItem");
    }



    private void Update()
    {
        if (currentDragItem)
        {
            screenPoint = dragCamera.WorldToScreenPoint(currentDragItem.transform.position);

            //float tilt = Input.GetAxisRaw("Rotate");
            //currentDragItem.transform.RotateAround(Vector3.zero, Vector3.up * tilt, rotateSmooth * Time.deltaTime);

            var currentDraggable = currentDragItem.GetComponent<Draggable>();

            bool canUpgrade = false;
            Tower upgradeTower = null;

            // show upgrade info
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                if (currentDraggable.GetComponent<Tower>())
                {
                    //RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
                    foreach (var hit in Physics2D.RaycastAll(mousePos2D, Vector2.zero))
                    {
                        //Debug.Log("hit " + hit.transform.gameObject);

                        var hitInfo = hit.transform.GetComponent<TowerInfoController>();
                        if (hitInfo)
                        {
                            if (hitInfo.transform!=currentDraggable.transform && hitInfo.canUpgradeWith(currentDraggable.GetComponent<Tower>()))
                            {

                                upgradeTower = hitInfo.GetComponent<Tower>();
                                if (!currentHoverTower || currentHoverTower!=upgradeTower)
                                {

                                    currentHoverTower = upgradeTower;
                                    currentDraggable.GetComponent<TowerInfoController>().init(upgradeTower);
                                }
                                currentDraggable.GetComponent<TowerInfoController>().showUpgradeInfo(upgradeTower);
                                currentDraggable.showEnableOverlay();
                                currentDraggable.showUpgradeOverlay();
                                canUpgrade = true;
                                break;
                            }
                        }
                    }
                    if (!canUpgrade)
                    {
                        currentDraggable.GetComponent<TowerInfoController>().hideUpgradeInfo();
                        currentDraggable.hideUpgradeOverlay();
                    }
                }
            }


            if (!canUpgrade)
            {
                if (currentHoverTower)
                {
                    currentDraggable.GetComponent<TowerInfoController>().init(currentDraggable.GetComponent<Tower>());
                    currentHoverTower = null;
                }
                bool canbuild = currentDraggable.canBuildItem();
                if (!canbuild)
                {
                    currentDraggable.showDisableOverlay();
                }
                else
                {
                    currentDraggable.showEnableOverlay();
                }
            }
                Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 mousePosition = dragCamera.ScreenToWorldPoint(newPosition);

                if (currentDraggable.isGridBased)
                {
                    //map to grid position
                    //
                    mousePosition = Utils.snapToGridCenter(mousePosition);
                }
                currentDragItem.transform.position = mousePosition;




            //start upgrade
            if (Input.GetMouseButtonDown(0))
            {
                if (upgradeTower)
                {
                    upgradeTower.upgrade();
                    currentDragItem.GetComponent<TowerInfoController>().hideInfo();
                    currentDragItem.GetComponent<TowerInfoController>().hideUpgradeInfo();
                    currentDraggable.hideUpgradeOverlay();

                    Destroy(currentDraggable.gameObject);
                    return;

                }
            }
        }




        if (Input.GetMouseButtonDown(1))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                print("on ui");
                return;
            }
            Debug.Log("mouse down");

            if (currentDragItem)
            {
                currentDragItem.GetComponent<Draggable>().removeDragItem();
                return;
            }



            //LayerMask mask = LayerMask.GetMask("item");
            //if (currentDragItem == null && isInBuildMode)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //    foreach (var hit in Physics.RaycastAll(ray, 1000, mask))
            //    {
            //        Debug.Log("hit " + hit.transform.gameObject);
            //        var hitItem = hit.transform.GetComponent<DraggableItem>();
            //        if (hitItem)
            //        {
            //            Doozy.Engine.GameEventMessage.SendEvent("ItemAction");
            //            selectItem(hit.transform.gameObject);
            //            return;
            //        }
            //    }
            //}
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentSelectedInfo)
            {
                currentSelectedInfo.hideInfo();
                currentSelectedInfo = null;
            }
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                print("on ui");
                return;
            }

            if (selectedItem && currentDragItem)
            {
                Debug.LogError("could only hold one item");
                return;
            }
            if (currentDragItem)
            {
                currentDragItem.GetComponent<Draggable>().tryBuild();
                return;
            }


            //select the focus target
            //if (currentDragItem == null && isInBuildMode)
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                //RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
                foreach (var hit in Physics2D.RaycastAll(mousePos2D, Vector2.zero))
                {
                    Debug.Log("hit " + hit.transform.gameObject);
                    var hitItem = hit.transform.GetComponent<SelectToFocusTarget>();
                    if (hitItem)
                    {

                        if (currentFocusTarget)
                        {
                            currentFocusTarget.deselect();
                        }
                        currentFocusTarget = hitItem;
                        currentFocusTarget.select();
                        return;
                    }

                    var hitInfo = hit.transform.GetComponent<TowerInfoController>();
                    if (hitInfo)
                    {
                        if (currentSelectedInfo)
                        {
                            currentSelectedInfo.hideInfo();
                        }
                        currentSelectedInfo = hitInfo;
                        currentSelectedInfo.showInfo();
                        return;
                    }
                }
            }



            if (currentSelectedInfo)
            {
                currentSelectedInfo.hideInfo();
                currentSelectedInfo = null;
            }
            if (currentFocusTarget)
            {
                currentFocusTarget.deselect();
                currentFocusTarget = null;
            }

            //if (selectedItem)
            //{
            //    deselectIem();

            //    Doozy.Engine.GameEventMessage.SendEvent("CancelItemAction");
            //}
            //if (currentDragItem == null)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //    foreach (var hit in Physics.RaycastAll(ray))
            //    {
            //        Debug.Log("hit " + hit.transform.gameObject);
            //        var hitItem = hit.transform.GetComponent<DraggableItem>();
            //        if (hitItem)
            //        {
            //            if (hitItem.room.isEditing)
            //            {
            //                startDragItem(hitItem.gameObject);
            //                return;
            //            }
            //        }
            //    }
            //}

            return;
        }
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

    public void removeCurrentSelect()
    {
        //pop up 
        selectedItem.GetComponent<Draggable>().removeDragItem();
    }
    public void moveCurrentSelect()
    {
        currentDragItem = selectedItem;
        currentDragItem.GetComponent<Draggable>().isDragging = true;
        selectedItem = null;
    }
    public void cancelSelect()
    {
        selectedItem = null;
    }
}