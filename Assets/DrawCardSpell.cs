using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardSpell : InstantSpell
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        BattleCardManager.Instance.drawCard(2);


        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
