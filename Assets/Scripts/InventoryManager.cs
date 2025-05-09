using UnityEngine;


public class InventoryManager : MonoBehaviour
{
   
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject[] ItemPrefabs { get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private ItemData[] itemData;
    public ItemData[] ItemData { get { return itemData; } set { itemData = value; } }

    public const int MAXSLOT = 16;

    public static InventoryManager instance;
    void Awake()
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

    public bool AddItem(Character character, int id)
    {
        if (character.InventoryItems == null)
        {
            Debug.LogError("InventoryItems is NULL for character " + character.name);
            return false;
        }
        Item item = new Item(itemData[id]);

        for (int i =  0; i < character.InventoryItems.Length; i++)
        {
            if (character.InventoryItems[i] == null)
            {
                character.InventoryItems[i] = item;
                Debug.Log("Item added: " + item.ItemName + " to " + character.name);
                Debug.Log($"Adding item to: {character.name} (InstanceID: {character.GetInstanceID()})");
                return true;
            }
                
        }
        Debug.Log("Inventory . Full");
        return false;

      
    }
}
