using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    public RectTransform SelectionBox
    {
        get { return selectionBox; }
    }

    [SerializeField] private Toggle togglePauseUnpause;

    public static UIManager instance;

    [SerializeField]
    private Toggle[] toggleMagic;
    public Toggle[] ToggleMagic { get { return toggleMagic; }}

    [SerializeField]
    private int curToggleMagicID = -1;

    [SerializeField]
    private GameObject blackImage;

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject itemUIPrefab;

    [SerializeField]
    private GameObject[] slots;

    [SerializeField]
    private GameObject grayImage;

    [SerializeField]
    private GameObject itemDialog;

    [SerializeField]
    private ItemDrag curItemDrag;

    [SerializeField]
    private int curSlotId;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            togglePauseUnpause.isOn = !togglePauseUnpause.isOn;
        }
    }

    public void ToggleAI(bool isOn)
    {
        foreach (Character member in PartyManager.instance.Members)
        {
            AttackAI ai = member.gameObject.GetComponent<AttackAI>();
            if (ai != null)
            {
                ai.enabled = isOn;
            }
        }
    }

    public void SelectAll()
    {
        foreach (Character member in PartyManager.instance.Members)
        {
            if (member.CurHP > 0)
            {
                member.ToggleRingSelection(true);
                PartyManager.instance.SelectChars.Add(member);
            }
        }
    }

    public void PauseUnpause(bool isOn)
    {
        Time.timeScale = isOn ? 0 : 1;
    }

    /*public void ShowMagicToggles()
    {
        if (PartyManager.instance.SelectChars.Count <= 0)
            return;

        //Show . Magic. skill . only. the. single .selected .hero
        Character hero = PartyManager.instance.SelectChars[0];

        for (int i = 0; i < hero.MagicSkills.Count; i++)
        {
            toggleMagic[i].interactable = true;
            toggleMagic[i].isOn = false;
            toggleMagic[i].GetComponentInChildren<Text>().text = hero.MagicSkills[i].Name;
        }
    }*/

    public void ShowMagicToggles()
    {
        if (PartyManager.instance.SelectChars.Count <= 0)
            return;

        
        Character hero = PartyManager.instance.SelectChars[0];

        
        for (int i = 0; i < toggleMagic.Length; i++)
        {
            toggleMagic[i].interactable = false; 
            toggleMagic[i].isOn = false; 
            toggleMagic[i].GetComponentInChildren<Text>().text = ""; 
        }

        
        for (int i = 0; i < hero.MagicSkills.Count && i < toggleMagic.Length; i++)
        {
            toggleMagic[i].interactable = true;
            toggleMagic[i].isOn = false;
            toggleMagic[i].GetComponentInChildren<Text>().text = hero.MagicSkills[i].Name;
            toggleMagic[i].targetGraphic.GetComponent<Image>().sprite = hero.MagicSkills[i].Icon;
        }
    }


    public void SelectMagicSkill(int i)
    {
        curToggleMagicID = i;
        PartyManager.instance.HeroSelectMagicSkill(i);
    }

    public void IsOnCurToggleMagic(bool flag)
    {
        toggleMagic[curToggleMagicID].isOn = flag;
    }

    public void ToggleInventoryPanel()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(true);
            blackImage.SetActive(true);
            ShowInventory();
        }
        else
        {
            inventoryPanel.SetActive(false);
            blackImage.SetActive(false);
            ClearInventory();
        }

    }

    public void ClearInventory()
    {
        //Clear. Slots
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                Transform child = slots[i].transform.GetChild(0);
                Destroy(child.gameObject);

            }
        }
    }
    public void ShowInventory()
    {
        // Check if any character is selected
        if (PartyManager.instance.SelectChars.Count <= 0)
            return;

        // Show inventory of the first selected character
        Character hero = PartyManager.instance.SelectChars[0];

        Debug.Log($"Showing inventory for: {hero.name}");


        // Show items
        for (int i = 0; i < hero.InventoryItems.Length; i++)
        {
            if (hero.InventoryItems[i] != null)
            {
                Debug.Log($"Item at slot {i}: {hero.InventoryItems[i].ItemName}");

                GameObject itemObj = Instantiate(itemUIPrefab, slots[i].transform);
                ItemDrag itemDrag = itemObj.GetComponent<ItemDrag>();

                itemDrag.UIManager = this;

                itemDrag.Item = hero.InventoryItems[i];
                itemDrag.IconParent = slots[i].transform;
                itemDrag.Image.sprite = hero.InventoryItems[i].Icon;
            }
        }
    }

    private void InitSlots()
    {
        for (int i = 0; i < InventoryManager.MAXSLOT; i++)
        {
            slots[i].GetComponent<InventorySlot>().ID = i;
        }
    }

    public void SetCurItemInUse(ItemDrag itemDrag, int index)
    {
        curItemDrag = itemDrag;
        curSlotId = index;
    }

    public void ToggleItemDialog(bool flag)
    {
        grayImage.SetActive(flag);
        itemDialog.SetActive(flag);
    }

    public void DeleteItemIcon()
    {
        Destroy(curItemDrag.gameObject);//destroy. Icon
    }

    public void ClickDrinkConsumable() //Map.with. Button. "Use"
    {
        InventoryManager.instance.DrinkConsumableItem(curItemDrag.Item, curSlotId);
        DeleteItemIcon();
        ToggleItemDialog(false);
    }

}
