using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshManager : Singleton<NavMeshManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void updateNavMesh()
    {

        AstarPath.active.Scan();
        NavVisualizationManager.Instance.UpdateNav();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
