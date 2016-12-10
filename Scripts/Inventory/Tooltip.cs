using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour
{
    private Item item;
    private string data;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if(tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#0473f0><b>" + item.Title + "\n\n</b></color>" + item.Description + "\n\n";
        switch(item.Type)
        {
            case "Note":
                TextAsset contents = Resources.Load<TextAsset>("Text/" + item.Slug);
                if (contents.text.Length < 20)
                    data += "<size=15><i>\"" + contents.text + "...\"</i></size>";
                else
                    data += "<size=15><i>\"" + contents.text.Substring(0, 20) + "...\"</i></size>";
                break;

            case "Lantern":
                data += "Fuel: " + item.Value;
                break;

            default:
                break;
        }
        //data += "\n\n<color=#ffff00ff><size=25><i>" + item.Type + "</i></size></color>";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

    public void Show()
    {
        CanvasGroup canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void Hide()
    {
        CanvasGroup canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
