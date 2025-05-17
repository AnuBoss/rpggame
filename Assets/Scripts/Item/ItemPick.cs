using UnityEngine;

public class ItemPick : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Item Item { get { return item; } }

    private InventoryManager inventoryManager;
    private PartyManager partyManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(Item item, InventoryManager invManeger, PartyManager ptyManager)
    {
        this.item = item;
        inventoryManager = invManeger;
        partyManager = ptyManager;
    }
    private void PickUpItem(Character hero)
    {
        if (inventoryManager.AddItem(hero, item.ID))
            Destroy(gameObject);
        Debug.Log("Destroy " + item);
    }

    private void OnMouseDown()
    {
        Debug.Log("Pick Up");
        if (partyManager.SelectChars.Count > 0)
            PickUpItem(partyManager.SelectChars[0]);
    }
}
