using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class HideOnMouseExit : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public static HideOnMouseExit Current;

    public bool InContextMenu = false;

    public HideOnMouseExit()
    {
        Current = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InContextMenu = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InContextMenu = false;
        StartCoroutine(waitBeforeHide());
    }

    IEnumerator waitBeforeHide()
    {
        yield return new WaitForSeconds(0.5f);
        if(!InContextMenu)
            ContextMenu.Current.Hide();
    }
}
