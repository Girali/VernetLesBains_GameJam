using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Transform view = null;
    private Rigidbody rb;

    private bool canMove = true;
    [SerializeField]
    private float speed = 5f;
    private float currentLookSensitivity = 2f;
    private float currentCameraRotationX = 0f;
    private float cameraRotationLimit = 85f;
    private Vector3 move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        AppController.Instance.CursorLocked = true;
    }

    public void Mouvement(float x, float z)
    {
        if (canMove)
        {
            Vector3 horizontalMove = transform.right * x;
            Vector3 verticalMove = transform.forward * z;

            move = (horizontalMove + verticalMove) * speed;
            move.y = rb.velocity.y;
        }
        else
        {
            move = Vector3.Lerp(move,Vector3.zero,0.5f);
            move.y = rb.velocity.y;
        }
    }

    public void CameraMouvement(float y, float x)
    {
        if (canMove)
        {
            float yRot = y;
            Vector3 rotation = new Vector3(0f, yRot, 0f) * currentLookSensitivity;

            float xRot = x;
            float cameraRotationX = xRot * currentLookSensitivity;

            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            view.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    public void SwitchCursorState()
    {
        AppController.Instance.CursorLocked = !AppController.Instance.CursorLocked;
        canMove = AppController.Instance.CursorLocked;
    }

    private void FixedUpdate()
    {
        rb.velocity = move;
    }
}
