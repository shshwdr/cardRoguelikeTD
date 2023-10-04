using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NavVisualizationManager : Singleton<NavVisualizationManager>
{
    private LineRenderer line;

    Seeker seeker;

    private Vector3 start;
    private Vector3 end;

    private void Awake()
    {
        line = GetComponentInChildren<LineRenderer>();
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {

            line.positionCount = p.vectorPath.Count;
            line.SetPositions(p.vectorPath.ToArray());
            
            
        }
        else
        {
            Debug.Log("path error!");
        }
    }
    
    public void SetNav(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
        seeker = GetComponent<Seeker>();
        seeker.StartPath(start, end, OnPathComplete);
    }

    public void UpdateNav()
    {
        seeker.StartPath(start, end, OnPathComplete);
    }
}
