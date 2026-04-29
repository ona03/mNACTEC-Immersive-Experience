using UnityEngine;

public class iniciJoc : MonoBehaviour
{
    // public GameObject player;
    private MeshRenderer mRenderer;
    private Camera cameraJugador;
    private RaycastLook raycastLook;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject camJ = GameObject.FindWithTag("MainCamera");
    
        if (camJ != null)
        {
            cameraJugador = camJ.GetComponent<Camera>();
            raycastLook = camJ.GetComponent<RaycastLook>();
        }
        if (cameraJugador != null && raycastLook != null)
        {
            cameraJugador.enabled = false;
            raycastLook.enabled = false;
        } 
    }
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject capsule = null;

        if (player != null) 
        {
            Transform capsuleTransform = player.transform.Find("Capsule Mesh");
            if (capsuleTransform != null) capsule = capsuleTransform.gameObject;
        }

        if (capsule != null) mRenderer = capsule.GetComponent<MeshRenderer>();
        if (mRenderer != null) mRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void finalCam()
    {
        this.gameObject.SetActive(false);
        mRenderer.enabled = true;
        cameraJugador.enabled = true;
        raycastLook.enabled = true;
    }
}
