using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Current;

    public int MaxRayDistance = 10;
    public Text ObjectText;

    private bool canInteract;
    private Interactive obj;

    public PlayerInteract()
    {
        Current = this;
    }

    void Start()
    {
        ObjectText.text = "";
        canInteract = false;
    }

    void FixedUpdate()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        //reset text to blank each frame, will be written over by code below if required
        ObjectText.text = "";
        canInteract = false;

        RaycastHit hit;
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(x, y)), Camera.main.ScreenToWorldPoint(new Vector3(x, y, MaxRayDistance)), Color.red);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(x, y)), out hit, MaxRayDistance))
        {
            if (hit.transform.CompareTag("Interactive"))
            {
                obj = hit.transform.GetComponent<Interactive>();
                ObjectText.text = obj.NameOfObject;

                canInteract = true;

                if (Input.GetKey(KeyCode.E))
                {
                    //print("Hit: " + (int)hit.distance);
                    obj.Interact();
                }
            }
        }

    }

    public void AttemptInteract()
    {
        if (canInteract)
        {
            obj.Interact();
        }
            
    }
}
