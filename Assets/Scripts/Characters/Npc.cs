using UnityEngine;
using System.Collections.Generic;
public class Npc : Character
{
    [SerializeField]
    private List<Quest> questToGive = new List<Quest>();
    public List<Quest> QuestToGive { get { return questToGive; } set { questToGive = value; } }

    [SerializeField]
    private bool isShopKeeper;
    public bool IsShopKeeper { get { return isShopKeeper; } }

    [SerializeField]
    private List<Item> shopItems = new List<Item>();
    public List<Item> ShopItems { get { return shopItems; } set { shopItems = value; } }

    [SerializeField]
    private int npcMoney = 3000;
    public int NpcMoney { get { return npcMoney; } set { npcMoney = value; } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Quest CheckQuestList(QuestStatus status)
    {
        foreach (Quest quest in questToGive)
        {
            if (quest.Status == status)
                return quest;
        }
        return null;

        

    }

   

}
