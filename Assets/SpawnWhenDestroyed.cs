using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnWhenDestroyed : BehaviorWhenDestroyed
{

    public Transform spawnParent;
    public int spawnCount = 2;
    public GameObject[] spawnItems;
    public bool isRandomSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onDestroyed(GameObject whoKilledMe)
    {
        base.onDestroyed(whoKilledMe);

        //StartCoroutine(spawnGradually());
        var spawnTransforms = Utils.GetRandomItemsFromList(spawnParent.GetComponentsInChildren<Transform>().ToList(), spawnCount);

        List<Vector3> vecs = new List<Vector3>();

        foreach (var tran in spawnTransforms)
        {
            vecs.Add(tran.position);
        }
        CustomerManager.Instance.spawnEnemies(vecs, spawnItems[0]);
        //    var spawnItem = spawnItems[0];
        //    var go = Instantiate(spawnItem, tran.position, Quaternion.identity, transform.parent);

        //    go.GetComponent<Customer>().init(GetComponent<Customer>().finalTarget, CustomerManager.Instance.CustomerInfoDict[spawnItem.name]);
        //}
    }

    //IEnumerator spawnGradually()
    //{
    //    var spawnTransforms = Utils.GetRandomItemsFromList(spawnParent.GetComponentsInChildren<Transform>().ToList(), spawnCount);
    //    foreach (var tran in spawnTransforms)
    //    {
    //        yield return null;
    //        var spawnItem = spawnItems[0];
    //        var go = Instantiate(spawnItem, tran.position, Quaternion.identity, transform.parent);

    //        go.GetComponent<Customer>().init(GetComponent<Customer>().finalTarget, CustomerManager.Instance.CustomerInfoDict[spawnItem.name]);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
