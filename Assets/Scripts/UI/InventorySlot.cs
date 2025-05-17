using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount > .0)
            return;

        GameObject objDrop = eventData.pointerDrag;
        ItemDrag item = objDrop.GetComponent<ItemDrag>();
        item.IconParent = transform;
    }
}
