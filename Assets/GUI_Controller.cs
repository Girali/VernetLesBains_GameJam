using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Controller : MonoBehaviour
{
    private static GUI_Controller instance;
    public static GUI_Controller Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GUI_Controller>();
            return instance;
        }
    }

    [SerializeField]
    private Jun_TweenRuntime fade;
    [SerializeField]
    private Jun_TweenRuntime flash;

    public UnityEngine.Events.UnityAction fadeEvent;

    public void FadeEvent()
    {
        fadeEvent();
    }

    public void Flash()
    {
        flash.gameObject.SetActive(true);
        flash.Play();
    }

    public void FadeBlack(bool b)
    {
        if (b)
        {
            fade.Play();
        }
        else
        {
            fade.Rewind();
        }
    }
}
