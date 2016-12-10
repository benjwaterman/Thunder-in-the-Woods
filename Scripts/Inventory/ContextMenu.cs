using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ContextMenu : MonoBehaviour
{
    public static ContextMenu Current;
    public static bool Open;

    public GameObject ContextMenuObject;
    public GameObject ContextMenuButton;

    public ContextMenu()
    {
        Current = this;
    }

    void Start()
    {
        ContextMenuObject.SetActive(false);
    }

    public void Show(Item item)
    {
        Open = true;

        ContextMenuObject.SetActive(true);
        ContextMenuObject.transform.position = Input.mousePosition;

        switch (item.Type)
        {
            case "Note":
                GameObject button = Instantiate(ContextMenuButton);
                button.transform.SetParent(ContextMenuObject.transform);
                button.name = "ReadButton";
                button.GetComponentInChildren<Text>().text = "Read";
                button.GetComponent<Button>().onClick.AddListener(delegate { NoteOpen.Current.Show(item.ThisGameObject.GetComponent<Note>()); });

                button = Instantiate(ContextMenuButton);
                button.transform.SetParent(ContextMenuObject.transform);
                button.name = "TestButton";
                button.GetComponentInChildren<Text>().text = "Test";
                break;

            case "Lantern":
                break;

            default:
                break;
        }
    }

    public void Hide()
    {
        Open = false; 

        for (int i = 0; i < ContextMenuObject.transform.childCount; i++)
            Destroy(ContextMenuObject.transform.GetChild(i).gameObject);

        ContextMenuObject.SetActive(false);
    }
}
