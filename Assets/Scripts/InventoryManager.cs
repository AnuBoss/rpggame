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
}
