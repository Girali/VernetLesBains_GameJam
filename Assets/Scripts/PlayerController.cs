using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMotor playerMotor;
    [SerializeField]
    private CameraController cameraController;

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        playerMotor.CameraMouvement(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        playerMotor.Mouvement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(cameraController.Capture());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerMotor.SwitchCursorState();
        }
    }
}
