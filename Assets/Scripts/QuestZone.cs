using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestZone : MonoBehaviour
{
    public UnityEvent eventOnEnter;
    public UnityEvent eventOnExit;
    public UnityEvent eventOnPictureTaken;
    public UnityEvent eventOnComplete;
    public UnityEvent eventOnExitZoneComplete;
    [Space(30)]
    public AudioSource audioSource;
    public Transform view;
    public Transform pos;
    public bool completed = false;

    public void OnEnable()
    {
        AppController.Instance.zoneCompleteEvent += EventOnComplete;
    }

    public void OnDisable()
    {
        AppController.Instance.zoneCompleteEvent -= EventOnComplete;
    }

    public void EventOnComplete()
    {
        completed = true;
        eventOnComplete.Invoke();
    }

    public void eventOnZoneExitCompleted()
    {
        completed = true;
        eventOnExitZoneComplete.Invoke();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.Play();
            AppController.Instance.currentQuestZone = this;
            eventOnEnter.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.Stop();
            AppController.Instance.currentQuestZone = null;
            eventOnExit.Invoke();

            if (completed)
            {

            }
        }
    }

    public void TakePicture(PlayerController pc)
    {
        if (!completed)
        {
            Vector3 v = (view.position - (pos.position + (pc.Cam.transform.position - pos.position))).normalized;
            Vector3 vv = pc.Cam.transform.forward;
            float angle = Vector3.Dot(v, vv);

            Vector3 p = pc.transform.position - pos.position;
            p.y = 0;

            if (angle > 0.8f && p.magnitude < 0.75f)
            {
                eventOnPictureTaken.Invoke();
                AppController.Instance.mainQuestPhoto = true;
            }
        }
    }
}
