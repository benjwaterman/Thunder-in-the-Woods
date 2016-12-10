using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject Tooltip;

    void Update()
    {
        if (Input.GetKeyDown((KeyCode.Escape)))
        {
            if (GameManager.Paused)
            {
                GameManager.Current.ResumeGame();
            }

            else if (!GameManager.Paused)
            {
                if (NoteOpen.Open)
                    NoteOpen.Current.Hide();

                else if (Inventory.Open)
                {
                    Inventory.Current.Hide();
                    Tooltip.SetActive(false);
                }

                else
                {
                    GameManager.Current.PauseGame();
                }
            }
        }

        if (Input.GetKeyDown((KeyCode.E)) && !GameManager.Paused)
        {
            if (NoteOpen.Open && !Inventory.Open)
                NoteOpen.Current.Take();

            else
                PlayerInteract.Current.AttemptInteract();
        }

        if (Input.GetKeyDown((KeyCode.I)))
        {
            if (!GameManager.Paused)
            {
                if (Inventory.Open)
                {
                    Inventory.Current.Hide();
                }

                else if (!Inventory.Open)
                {
                    Inventory.Current.Show();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!HideOnMouseExit.Current.InContextMenu)
                ContextMenu.Current.Hide();
        }
    }
}
