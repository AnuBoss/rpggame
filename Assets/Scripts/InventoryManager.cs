using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{

    public const int MAXSLOT = 18;
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject[] ItemPrefabs { get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData { get { return itemData; } set { itemData = value; } }

    public static InventoryManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AddItem(Character chracter, int id)
    {
        Item item = new Item(itemData[id]);
        for (int i = 0; i < chracter.InventoryItems.Length; i++)
        {
            if (chracter.InventoryItems[i] == null)
            {
                chracter.InventoryItems[i] = item;
                return true;
            }

        }
        return false;

    }

    public void SaveItemBag(int index, Item item)
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;
        PartyManager.instance.SelectChars[0].InventoryItems[index] = item;
        switch (index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].EquipShield(item);
                break;
            case 17:
                PartyManager.instance.SelectChars[0].EquipWeapon(item);

                break;
        }
    }
    public void RemoveItemInBag(int index)
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;
        PartyManager.instance.SelectChars[0].InventoryItems[index] = null;
        switch (index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].UnEquipShield();
                break;
            case 17:
                PartyManager.instance.SelectChars[0].UnEquipWeapon();
                break;
        }
    }
    private void SpawnDropItem(Item item, Vector3 pos)
    {
        int id;
        switch (item.Type)
        {
            case ItemType.Consumable:
                id = 1;
                break;
            default:
                id = 0;
                break;
        }
        GameObject itemObj = Instantiate(ItemPrefabs[id], pos, Quaternion.identity);
        itemObj.AddComponent<ItemPick>();

        ItemPick itemPick = itemObj.GetComponent<ItemPick>();
        itemPick.Init(item, instance, PartyManager.instance);
    }
    public void SpawnDropInventory(Item[] item, Vector3 pos)
    {
        Vector3 spawnPos = pos + new Vector3(0, 5f, 0);

        for (int i = 0; i < 1; i++)
        {
            if (item[i] != null)
                SpawnDropItem(item[i], pos);
        }
    }
    public void DrinkConsumableItem(Item item, int slotId)
    {
        string s = string.Format("Drink: {0}", item.ItemName);
        Debug.Log(s);
        if (PartyManager.instance.SelectChars.Count > 0)
        {
            PartyManager.instance.SelectChars[0].Recover(item.Power);
            RemoveItemInBag(slotId);
        }
    }

    public bool CheckPartyForItem(int id)
    {
        Item item = new Item(itemData[id]);
        Debug.Log(item.ItemName);

        List<Character> party = PartyManager.instance.Members;

        foreach(Character hero in party)
        {
            for (int i = 0; i < hero.InventoryItems.Length; i++)
            {
                Debug.Log(hero.InventoryItems[i].ItemName);
                if(hero.InventoryItems[i].ID == item.ID)
                   return true;
            }
        }
        return false;
    }
}
