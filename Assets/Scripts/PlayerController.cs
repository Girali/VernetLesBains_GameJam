using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMotor playerMotor;
    [SerializeField]
    private CameraController cameraController;
    private bool inPhoto = false;
    private float photoValue = 10;
    private bool photographReady = true;
    [SerializeField]
    private float photoScrollSpeed = 0.5f;
    [SerializeField]
    private float photoScrollReady = 10f;

    [SerializeField]
    private Camera cam;

    public Camera Cam
    {
        get
        {
            return cam;
        }
    }

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        playerMotor.CameraMouvement(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        playerMotor.Mouvement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (!inPhoto)
        {
            if (!GUI_Controller.Instance.photoGraphLocked)
            {
                if(photographReady)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(cameraController.Capture());
                        playerMotor.SwitchCursorState();
                        photoValue = 0;
                        photographReady = false;
                        inPhoto = true;
                        AppController.Instance.TakePicture(this);
                    }
                }

                photoValue += Mathf.Abs(Input.mouseScrollDelta.y) * photoScrollSpeed;
                if (photoValue > photoScrollReady)
                    photoValue = photoScrollReady;

                if (!photographReady && photoValue >= photoScrollReady)
                {
                    photographReady = true;
                    GUI_Controller.Instance.ShowHidePhotograph(false);
                    GUI_Controller.Instance.RotatePhotograph(1f);
                }

                GUI_Controller.Instance.RotatePhotograph(photoValue / photoScrollReady);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                playerMotor.SwitchCursorState();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                inPhoto = false;
                GUI_Controller.Instance.ClosePhoto();
                playerMotor.SwitchCursorState();

                if (AppController.Instance.mainQuestPhoto)
                {
                    AppController.Instance.mainQuestPhoto = false;
                    if (AppController.Instance.zoneCompleteEvent != null)
                        AppController.Instance.zoneCompleteEvent();
                }
            }
        }
    }
}
