using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Draggable : GroundItem
{
    public bool isDragging = true;
    Vector3 screenPoint;
    Camera dragCamera;
    Material material;
    public Renderer renderer;
    public Renderer overlay;
    public string type;
    public InfoBase info;
    public bool isBuilt = false;


    public void changeOverlayColor(Color color)
    {
        overlay.material.color = color;
    }

    public void showEnableOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(true);
            changeOverlayColor(Color.green);
        }
        else
        {

            renderer.material.color = Color.green;
        }
    }

    public void showDisableOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(true);
            changeOverlayColor(Color.red);
        }
        else
        {

            renderer.material.color = Color.red;
        }
    }

    public void showActiveOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(true);
            changeOverlayColor(Color.yellow);
        }
        else
        {
            renderer.material.color = Color.yellow;
        }
    }

    public void hideOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(false);
        }
        else
        {

            renderer.material.color = Color.white;
        }

    }


    public void consumeRequirements()
    {
        InfoWithRequirementBase infoWithRequirement = (InfoWithRequirementBase)info;
        var requirements = infoWithRequirement.requireResources;
        foreach(var req in requirements)
        {
            Inventory.Instance.consumeItem(req.key, req.amount);
        }
    }
    public void addBackRequirements()
    {
        InfoWithRequirementBase infoWithRequirement = (InfoWithRequirementBase)info;
        var requirements = infoWithRequirement.requireResources;
        foreach (var req in requirements)
        {
            Inventory.Instance.addItem(req.key, req.amount);
        }
    }
    

    public void Init(string t,InfoBase i)
    {
        type = t;
        info = i;
    }
    public  abstract bool canBuildItem();
    protected abstract void build();

    protected virtual void Start()
    {
        dragCamera = Camera.main;
        material = renderer.material;
        screenPoint = dragCamera.WorldToScreenPoint(gameObject.transform.position);
    }
    

    //public IEnumerator renderNavWholeAsync()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    NavMeshSurface nm = GameObject.FindObjectOfType<NavMeshSurface>();
    //    //nm.BuildNavMesh();
    //    nm.BuildNavMesh();
    //    ShowNavmesh.Instance.ShowMesh();

    //}
    protected virtual void Update()
    {
        
    }

    public void tryBuild()
    {
        bool canbuild = canBuildItem();
        if (canbuild)
        {
            isDragging = false;
            material.color = Color.white;
            //MouseManager.Instance.finishDragItem(gameObject);
            build();
        }
    }

    public virtual void removeDragItem()
    {
    }
}
