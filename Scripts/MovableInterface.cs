using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class MovableInterface : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private Vector2 offset;
    private GameObject targetInterface;

    void Start()
    {
        targetInterface = this.transform.parent.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = eventData.position - (Vector2)targetInterface.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        targetInterface.transform.position = eventData.position - offset;
    }

    public void OnDrag(PointerEventData eventData)
    {
        targetInterface.transform.position = eventData.position - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        targetInterface.transform.position = eventData.position - offset;
    }
}
