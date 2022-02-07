
using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleCharacter
{

    CharacterInfo info;
    int hp;

    public int currentHP()
    {
        return hp;
    }
    public BattleCharacter(CharacterInfo inf)
    {
        info = inf;
        hp = info.maxHP;
    }

    public void doDamage(int d)
    {
        hp -= d;
        EventPool.Trigger("updateCharacterHP");
    }
}