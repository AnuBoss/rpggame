using UnityEngine;


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
    }
    public void RemoveItemInBag(int index)
    {
        if (PartyManager.instance.SelectChars.Count == 0)
            return;
        PartyManager.instance.SelectChars[0].InventoryItems[index] = null;
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
}
