using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterInfo : BaseInfo
{
    public Dictionary<string,int> initialDeck;
    public int isUnlocked;


}

public class CharacterManager : Singleton<CharacterManager>
{
    public Dictionary<string, CharacterInfo> characterInfoDict = new Dictionary<string, CharacterInfo>();
    private void Awake()
    {
        var cards = CsvUtil.LoadObjects<CharacterInfo>("Character");
        foreach (var card in cards)
        {
            characterInfoDict[card.name] = card;
        }
        //get character detail
    }

    public CharacterInfo getCurrentCharacterInfo()
    {
        return characterInfoDict["hero"];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
