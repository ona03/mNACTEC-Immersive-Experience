using UnityEngine;
using System.Collections;

public class setActiveByAssociation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject associatedObject;
    public int tempsActivacio = 10; // Segons d'espera

    private bool activacioIniciada = false;
    private MeshRenderer elMeuRenderer;
    private BoxCollider elMeuCollider;

    void Start()
    {
        // Guardem els components per no haver de fer GetComponent cada frame (més eficient)
        elMeuRenderer = GetComponent<MeshRenderer>();
        // Comencem invisibles
        elMeuRenderer.enabled = false;

        if (GetComponent<BoxCollider>() != null)
        {
            elMeuCollider = GetComponent<BoxCollider>();
            elMeuCollider.enabled = false;
        }
    }

    void Update()
    {
        if (!activacioIniciada && associatedObject.GetComponent<MeshRenderer>().enabled)
        {
            StartCoroutine(ActivarAmbRetard());
            activacioIniciada = true;
        }
    }
    IEnumerator ActivarAmbRetard()
    {
        yield return new WaitForSeconds(tempsActivacio);
        
        elMeuRenderer.enabled = true;

        if (elMeuCollider != null)
        {
            elMeuCollider.enabled = true;
        }
    }
}
