using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Current;
    public static bool Open;

    public GameObject inventoryPanel;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public GameObject CloseButton;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private int slotAmount = 30;
    private ItemDatabase database;

    public Inventory()
    {
        Current = this;
    }

    void Start()
    {
        Open = false;
        database = GetComponent<ItemDatabase>();

        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());

            GameObject slot = Instantiate(inventorySlot);
            slot.name = "SlotID: " + i;
            slots.Add(slot);
            slots[i].GetComponent<ItemSlot>().ID = i;
            slot.transform.SetParent(slotPanel.transform);
        }

        //AddItem(0);
        //AddItem(2);
        //AddItem(2);
        //AddItem(2);
        //AddItem(2);

        Hide();
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        //item is stackable
        if (itemToAdd.Stackable && CheckInInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.Amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.Amount.ToString();

                    break;
                }
            }
        }

        //non stackable
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1) //empty slot
                {
                    items[i] = itemToAdd;
                    GameObject itemObject = Instantiate(inventoryItem);
                    ItemData itemData = itemObject.GetComponent<ItemData>();
                    itemData.ThisItem = itemToAdd;
                    itemData.SlotID = i;
                    itemData.Amount = 1;
                    itemObject.transform.SetParent(slots[i].transform);
                    itemObject.transform.position = itemObject.transform.parent.transform.position;
                    itemObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + itemToAdd.SpritePath);
                    //check if sprite is assigned, if not apply default missing sprite
                    if (itemObject.GetComponent<Image>().sprite == null)
                        itemObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/missing");
                    //set names in inspector
                    itemObject.name = itemToAdd.Title;
                    //itemObject.transform.parent.gameObject.name = "Slot Containing: " + itemToAdd.Title;

                    break;
                }
            }
        }
    }

    bool CheckInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return true;
        }

        return false;
    }

    public void Show()
    {
        CanvasGroup canvasGroup = inventoryPanel.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        CloseButton.SetActive(true);
        Open = true;

        GameManager.Current.FreezePlayer();
    }

    public void Hide()
    {
        if (NoteOpen.Open)
            NoteOpen.Current.Hide();

        CanvasGroup canvasGroup = inventoryPanel.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        CloseButton.SetActive(false);
        Open = false;

        GameManager.Current.UnFreezePlayer();
    }
}
