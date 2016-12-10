using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int ID;

    //if dropped on top of this object
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //get currently dragged item
            ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

            //if slot is empty
            if (Inventory.Current.items[ID].ID == -1)
            {
                Inventory.Current.items[droppedItem.SlotID] = new Item(); //remove item from old slot
                Inventory.Current.items[ID] = droppedItem.ThisItem; //assign item to new slot

                droppedItem.SlotID = ID;
            }

            //slot has other item in and not dropping on itself
            else if (droppedItem.SlotID != ID)
            {
                //get this slots item
                Transform item = this.transform.GetChild(0);
                item.GetComponent<ItemData>().SlotID = droppedItem.SlotID;
                //set that items parent and position to the currently held item's position and parent
                item.transform.SetParent(Inventory.Current.slots[droppedItem.SlotID].transform);
                item.transform.position = Inventory.Current.slots[droppedItem.SlotID].transform.position;

                //set currently held item's old slot = to the item that it is being swapped with
                Inventory.Current.items[droppedItem.SlotID] = item.GetComponent<ItemData>().ThisItem;
                //set this slot = to this item
                Inventory.Current.items[ID] = droppedItem.ThisItem;

                //set current items slot ID
                droppedItem.SlotID = ID;
            }
        }
    }
}
