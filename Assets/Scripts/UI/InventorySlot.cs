using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private int id;
    public int ID { get { return id; } set { id = value; } }

    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType
    { get { return itemType; } set { itemType = value; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = InventoryManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject objA = eventData.pointerDrag;
        ItemDrag itemDragA = objA.GetComponent<ItemDrag>();
        InventorySlot slotA = itemDragA.IconParent.GetComponent<InventorySlot>();


        /*if (itemType == ItemType.Shield && itemType == ItemType.Weapon)
        {
            if (itemDragA.Item.Type != itemType)
                return;
        }
        /*if (itemType == ItemType.Weapon)
        {
            if (itemDragA.Item.Type != itemType)
                return;
        }
        */
        if (transform.childCount > 0)
        {
            GameObject objB = transform.GetChild(0).gameObject;
            ItemDrag itemDragB = objB.GetComponent<ItemDrag>();

           /* if (slotA.ItemType == ItemType.Shield && itemType == ItemType.Weapon)
            {
                if (itemDragB.Item.Type != slotA.ItemType)
                    return;
            }
            inventoryManager.RemoveItemInBag(slotA.ID);
            */
            itemDragB.transform.SetParent(itemDragA.IconParent);
            itemDragB.IconParent = itemDragA.IconParent;
            inventoryManager.SaveItemBag(slotA.ID, itemDragB.Item);

            inventoryManager.RemoveItemInBag(id);
        }
       /* else//Slot . B . is . blank
        {
            inventoryManager.RemoveItemInBag(slotA.ID);
        }*/

        itemDragA.IconParent = transform;
        inventoryManager.SaveItemBag(id, itemDragA.Item);
    }
}
