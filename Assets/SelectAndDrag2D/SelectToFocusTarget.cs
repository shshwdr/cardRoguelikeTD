using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectToFocusTarget : MonoBehaviour
{
    public GameObject focusUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void select()
    {
        focusUI.SetActive(true);
    }
    public void deselect()
    {

        focusUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
