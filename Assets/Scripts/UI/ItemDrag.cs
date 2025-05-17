using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Item item;
    public Item Item
    {get { return item; } set { item = value; }}

    [SerializeField]
    private Transform iconParent;
    public Transform IconParent
    { get { return iconParent; } set { iconParent = value; } }

    [SerializeField]
    private Image image;
    public Image Image
    { get { return image; } set { image = value; } }

    private UIManager uiManager;
    public UIManager UIManager
    { get { return uiManager; } set { uiManager = value; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeingDrag");
        iconParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData evenData)
    {
        Debug.Log("Draging");
        transform.position = Input.mousePosition;

    }
    public void OnEndDrag(PointerEventData evenData)
    {
        Debug.Log("EmdDrag");
        transform.SetParent(iconParent);
        image.raycastTarget = true;

    }

    private int FindIndexOfSlotParent()
    {
        int id = iconParent.GetComponent<InventorySlot>().ID;
        return id;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Click on Item");
            if (item.Type == ItemType.Consumable)
            {
                uiManager.SetCurItemInUse(this, FindIndexOfSlotParent());
                uiManager.ToggleItemDialog(true);
            }
        }
    }
}
