
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect:MonoBehaviour
{

    [SerializeField] string cardType;
    public string type { get { return cardType.Length > 0 ? cardType : name; } }
    public bool isActivated = false;
    public GameObject renderObject;
    public virtual void activate()
    {
        isActivated = true;
    }
    protected virtual  void Awake()
    {
        if (!renderObject)
        {
            renderObject = transform.Find("render").GetChild(0).gameObject;
        }
    }

}