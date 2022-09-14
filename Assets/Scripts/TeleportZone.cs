using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private PlayerMotor player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<PlayerMotor>();
            player.CanMove = false;
            GUI_Controller.Instance.FadeBlack(true);
            GUI_Controller.Instance.fadeEvent += Teleport;
        }
    }

    public void Teleport()
    {
        player.transform.position = target.position;
        player.transform.rotation = target.rotation;
        GUI_Controller.Instance.fadeEvent -= Teleport;
        GUI_Controller.Instance.FadeBlack(false);
        player.CanMove = true;
    }
}
