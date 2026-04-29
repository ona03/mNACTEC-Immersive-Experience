/*using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
    }

    void Update()
    {
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
*/

using UnityEngine;
using UnityEngine.InputSystem; // Necessari per al nou sistema

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera mycamera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;

    void Awake()
    {
        mycamera = GetComponent<Camera>();
        if (mycamera)
        {
            defaultFOV = mycamera.fieldOfView;
        }
    }

    void Update()
    {
        if (!mycamera) return;

        // Verifiquem si el ratolí està actiu
        if (Mouse.current == null) return;

        // NOU SISTEMA: Llegim el valor de la roda del ratolí (Vector2)
        // El valor .y ens indica si rodem cap amunt o cap avall
        float scrollValue = Mouse.current.scroll.ReadValue().y;

        // Nota: Dividim per 120 perquè el nou sistema sol donar valors de 120 o -120 per clic
        if (scrollValue != 0)
        {
            currentZoom += (scrollValue / 120f) * sensitivity * 0.05f;
            currentZoom = Mathf.Clamp01(currentZoom);
        }

        // Apliquem el FOV resultant
        mycamera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}