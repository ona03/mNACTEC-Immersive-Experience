using UnityEngine;
using System.Collections;

public class setActiveByTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject associatedObject;
    public int tempsActivacio = 10; // Segons d'espera
    public float distanciaActivacio = 2f; // Distància per activar
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
        // if (!activacioIniciada && associatedObject.GetComponent<MeshRenderer>().enabled)
        // {
        //     //Debug.Log("Condició complerta! Iniciant corrutina...");
        //     StartCoroutine(ActivarAmbRetard());
        //     activacioIniciada = true;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activacioIniciada && other.gameObject == associatedObject)
        {
            //Debug.Log("Condició complerta! Iniciant corrutina...");
            StartCoroutine(ActivarAmbRetard());
            activacioIniciada = true;
        }
    }


    IEnumerator ActivarAmbRetard()
    {
        //Debug.Log("Corrutina iniciada, esperant " + tempsActivacio + " segons...");
        yield return new WaitForSeconds(tempsActivacio);
        
        //Debug.Log("Temps acabat! Activant components.");
        elMeuRenderer.enabled = true;

        if (elMeuCollider != null)
        {
            elMeuCollider.enabled = true;
        }
    }
}
