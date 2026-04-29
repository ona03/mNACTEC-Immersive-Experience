using UnityEngine;

public class LockView_Movement : MonoBehaviour
{
    // private GameObject player;
    public GameObject objecteTrigger;
    private bool locked = false;
    private FirstPersonMovement movPlayer;
    private FirstPersonLook lookPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        movPlayer = player.GetComponent<FirstPersonMovement>();
        lookPlayer = player.transform.Find("First Person Camera").GetComponent<FirstPersonLook>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(gameObject.activeSelf);
        if (objecteTrigger.activeInHierarchy && !locked)
        {
            BloquejarJugador(true);

            // locked = true;
            // player.GetComponent<FirstPersonMovement>().enabled = false;
            // player.transform.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = false;
        }
        else if (!objecteTrigger.activeInHierarchy && locked)
        {
            BloquejarJugador(false);

            // locked = false;
            // player.GetComponent<FirstPersonMovement>().enabled = true;
            // player.transform.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = true;
        }
    }
    void BloquejarJugador(bool estat)
    {
        locked = estat;

        // Desactivem/Activem els scripts de moviment
        // player.GetComponent<FirstPersonMovement>().enabled = !estat;
        
        movPlayer.enabled = !estat;
        // Busquem la càmera (assegura't que el nom sigui exactament aquest)
        // var cameraLook = player.transform.Find("First Person Camera").GetComponent<FirstPersonLook>();
        if (lookPlayer != null) lookPlayer.enabled = !estat;
        

        if (estat) 
        {
            // ALLIBERAR RATOLÍ per poder clicar el botó
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            // TORNAR A BLOQUEJAR EL RATOLÍ per jugar
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
