using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private RawImage photo;
    [SerializeField]
    private Image bakcgroundPhoto;

    [SerializeField]
    private RawImage photoTempIn;
    [SerializeField]
    private RawImage photoTempOut;
    public bool photoGraphLocked = false;
    private bool firstPhoto = true;

    [Space(20)]

    [SerializeField]
    private Image photo2d;
    [SerializeField]
    private Jun_TweenRuntime bakcgroundPhoto2d;

    [Space(20)]

    [SerializeField]
    private Jun_TweenRuntime fade;
    [SerializeField]
    private Jun_TweenRuntime flash;

    [SerializeField]
    private Jun_TweenRuntime photograph;

    public UnityEngine.Events.UnityAction fadeEvent;


    public void NewPhoto(Texture t)
    {
        Flash();
        photo.texture = t;

        if (AppController.Instance.mainQuestPhoto)
        {
            photo2d.GetComponent<Jun_TweenRuntime>().Play();
            bakcgroundPhoto2d.Play();
        }
        else
        {
            photo.GetComponent<Jun_TweenRuntime>().Play();
            bakcgroundPhoto.GetComponent<Jun_TweenRuntime>().Play();
        }

        photoGraphLocked = true;
    }

    public void ClosePhoto()
    {

        if (AppController.Instance.mainQuestPhoto)
        {
            photo2d.GetComponent<Jun_TweenRuntime>().Rewind();
            bakcgroundPhoto2d.Rewind();
        }
        else
        {
            photo.GetComponent<Jun_TweenRuntime>().Rewind();
            bakcgroundPhoto.GetComponent<Jun_TweenRuntime>().Rewind();
        }

        photoGraphLocked = true;

        if (firstPhoto)
        {
            photoTempIn.gameObject.SetActive(true);
            photoTempIn.GetComponent<Jun_TweenRuntime>().Play();
            photoTempIn.texture = photo.texture;
            firstPhoto = false;
        }
        else
        {
            photoTempOut.GetComponent<Jun_TweenRuntime>().Play();
        }
    }

    public void ShowHidePhotograph(bool b)
    {
        if(b)
            photograph.Play();
        else
            photograph.Rewind();
    }

    public void RotatePhotograph(float i)
    {
        photograph.transform.eulerAngles = new Vector3(0, 0, i * 360f);
    }

    public void PhotoTempEventIn()
    {
        Graphics.CopyTexture(photoTempIn.texture, photoTempOut.texture);
        photoTempOut.GetComponent<Jun_TweenRuntime>().PlayAtTime(0);
        photoTempOut.GetComponent<Jun_TweenRuntime>().StopPlay();
        photoTempIn.gameObject.SetActive(false);
        photoTempOut.gameObject.SetActive(true);
        photoGraphLocked = false;
        ShowHidePhotograph(true);
    }

    public void PhotoTempEventOut()
    {
        photoTempIn.texture = photo.texture;
        photoTempIn.GetComponent<Jun_TweenRuntime>().Play();
        photoTempOut.gameObject.SetActive(false);
        photoTempIn.gameObject.SetActive(true);
    }

    public void PhotoOnScreenEvent()
    {
        photoGraphLocked = false;
    }

    public void FadeEvent()
    {
        fadeEvent.Invoke();
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
