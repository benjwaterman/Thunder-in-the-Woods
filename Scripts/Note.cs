using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Note : Interactive
{
    public int ItemID;
    public Image NoteImage;
    public TextAsset NoteText;
    public TextMesh RealWorldText;

    void Start()
    {
        if(RealWorldText == null)
            RealWorldText = GetComponentInChildren<TextMesh>();

        RealWorldText.text = NoteText.text;
    }

    public override void Interact()
    {
        NoteOpen.Current.Show(this);
        NoteOpen.Current.ItemID = ItemID;
    }
}
