using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    private static AppController instance;

    public static AppController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AppController>();
            return instance;
        }
    }

    private bool cursorLocked = false;


    public bool CursorLocked
    {
        get
        {
            return cursorLocked;
        }

        set
        {
            cursorLocked = value;
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
