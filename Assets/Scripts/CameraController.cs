using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speedLerp = 1f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Texture2D photo;

    private bool lerping = true;

    void Update()
    {
        if (lerping)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speedLerp);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, speedLerp);
        }
    }



    public IEnumerator Capture()
    {
        gameObject.SetActive(true);

        yield return null;

        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;

        cam.Render();

        photo.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        photo.Apply();
        RenderTexture.active = activeRenderTexture;

        yield return null;

        GUI_Controller.Instance.NewPhoto(photo);

        gameObject.SetActive(false);

        //byte[] bytes = image.EncodeToPNG();
        //Destroy(image);

        //File.WriteAllBytes(Application.dataPath + fileCounter + ".png", bytes);
        //fileCounter++;
    }
}
