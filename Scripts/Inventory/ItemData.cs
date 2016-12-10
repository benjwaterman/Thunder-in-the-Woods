using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item ThisItem;
    public int Amount;
    public int SlotID;

    private Vector2 offset;
    private Tooltip tooltip;

    void Start()
    {
        tooltip = Inventory.Current.GetComponent<Tooltip>();
    }

    //when object this is attatched to is clicked on
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ContextMenu.Open)
            ContextMenu.Current.Hide();

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ThisItem != null)
            {
                offset = eventData.position - (Vector2)this.transform.position;
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (ThisItem != null)
            {
                ContextMenu.Current.Show(ThisItem);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ThisItem != null)
            {
                this.transform.SetParent(this.transform.parent.parent);
                this.transform.position = eventData.position - offset;
                GetComponent<CanvasGroup>().blocksRaycasts = false;

                this.gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ThisItem != null)
            {
                this.transform.position = eventData.position - offset;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ThisItem != null)
            {
                this.transform.SetParent(Inventory.Current.slots[SlotID].transform);
                this.transform.position = Inventory.Current.slots[SlotID].transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(ThisItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
