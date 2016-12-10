using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Current;
    public static bool Paused;
    public GameObject PauseMenu;
    public GameObject Crosshair;
    public GameObject InventoryPanel;

    private GameObject player;
    private CharacterMotor charMotor;
    private MouseLook mouseHor;
    private MouseLook mouseVert;

    public GameManager()
    {
        Current = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charMotor = player.GetComponent<CharacterMotor>();
        //left/right look on camera
        mouseHor = player.GetComponent<MouseLook>();
        //up/down look on camera
        mouseVert = Camera.main.GetComponent<MouseLook>();

        //turn off cursor and lock it to center of screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //turn off menus
        PauseMenu.SetActive(false);
    }

    public void FreezePlayer()
    {
        //turn on cursor and allow it to move within window
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;

        charMotor.canControl = false;
        mouseHor.enabled = false;
        mouseVert.enabled = false;
    }

    public void UnFreezePlayer()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        charMotor.canControl = true;
        mouseHor.enabled = true;
        mouseVert.enabled = true;
    }

    public void PauseGame()
    {
        //error checking, make sure note cant be up whilst paused
        NoteOpen.Current.Hide();

        Paused = true;
        FreezePlayer();
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        Crosshair.SetActive(false);
    }

    public void ResumeGame()
    {
        Paused = false;
        UnFreezePlayer();
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Crosshair.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }
}

