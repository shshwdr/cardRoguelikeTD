using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllResourcesRequiredController : MonoBehaviour
{
     ResourceRequirementCell[] requirementCell;
    // Start is called before the first frame update
    void Awake()
    {
        requirementCell = GetComponentsInChildren<ResourceRequirementCell>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(InfoBase info)
    {
        var allrequirements = ((InfoWithRequirementBase)info).requireResources;
        int minValue = Mathf.Min(allrequirements.Length, requirementCell.Length);
        int i;
        for (i = 0;i< minValue; i++)
        {
            requirementCell[i].gameObject.SetActive(true);
            requirementCell[i].Init(allrequirements[i]);
        }
        for(;i< requirementCell.Length; i++)
        {
            requirementCell[i].gameObject.SetActive(false);
        }
    }
}
