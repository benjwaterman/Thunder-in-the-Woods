using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoteOpen : MonoBehaviour
{
    public static NoteOpen Current;
    public static bool Open;

    public GameObject NotePanel;
    public int ItemID;

    private GameObject noteObject;
    private Image noteImage;
    private Text noteText;
    private GameObject noteTakeButton;
    //private Animator anim;

    public NoteOpen()
    {
        Current = this;
        Open = false;
    }

    void Start()
    {
        Open = false;

        noteImage = NotePanel.GetComponent<Image>();
        noteText = NotePanel.GetComponentInChildren<Text>();
        noteTakeButton = GameObject.Find("TakeButton");
        //anim = NotePanel.GetComponent<Animator>();

        Hide();
    }

    public void Show(Note note)
    {
        ContextMenu.Current.Hide();
        NotePanel.SetActive(true);

        noteObject = note.gameObject;

        noteImage.sprite = note.NoteImage.sprite;
        noteText.text = note.NoteText.text;

        //anim.SetTrigger("Normal");

        if (Inventory.Open)
            noteTakeButton.SetActive(false);
        else
            noteTakeButton.SetActive(true);

        Open = true;

        GameManager.Current.FreezePlayer();
    }

    public void Hide()
    {
        //anim.SetTrigger("Disabled");
        NotePanel.SetActive(false);

        Open = false;

        if (!Inventory.Open)
            GameManager.Current.UnFreezePlayer();
    }

    public void Take()
    {
        //anim.SetTrigger("Taken");
        ItemDatabase.Current.FetchItemByID(ItemID).ThisGameObject = noteObject;
        Inventory.Current.AddItem(ItemID);
        noteObject.SetActive(false);
        NotePanel.SetActive(false);

        Open = false;

        if (!Inventory.Open)
            GameManager.Current.UnFreezePlayer();
    }
}
