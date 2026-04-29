using UnityEngine;

public class setActiveChild : MonoBehaviour
{
    public GameObject objectePare; // Assigna aquest camp al inspector o deixa-ho buit per usar aquest mateix objecte
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform fill in objectePare.transform)
        {
            fill.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivarTotsElsFills()
    {
        // Si no hem assignat un pare manualment, usem aquest mateix
        if (objectePare == null) objectePare = this.gameObject;

        // Recorrem tots els fills (el Transform és el que sap quins fills té)
        foreach (Transform fill in objectePare.transform)
        {
            fill.gameObject.SetActive(true);
        }

        // Debug.Log("S'han activat tots els fills de: " + objectePare.name);
    }
}
