using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : Singleton<BattleManager>
{

    BattleCharacter battleCharacter;
    // Start is called before the first frame update
    void Start()
    {
        BattleCardManager.Instance.drawHand();
        battleCharacter = new BattleCharacter(CharacterManager.Instance.characterInfoDict["hero"]);

        EventPool.Trigger("updateCharacterHP");
    }

    public int characterLife()
    {
        return battleCharacter.currentHP();
    }
    public void getDamage(int d)
    {
        battleCharacter.doDamage(d);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
