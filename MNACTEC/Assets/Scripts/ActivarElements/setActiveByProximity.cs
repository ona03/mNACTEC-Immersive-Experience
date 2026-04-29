using UnityEngine;
using System.Collections.Generic;

public class setActiveByProximity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> objActivacio;
    // private float distance;
    public float distanciaMinima = 5f;
    private MeshRenderer mRenderer;
    private BoxCollider bCollider;
    void Start()
    {
        //jugador = GameObject.FindWithTag("Player");
        mRenderer = GetComponent<MeshRenderer>();
        bCollider = GetComponent<BoxCollider>();

        if (mRenderer != null) mRenderer.enabled = false;
        if (bCollider != null) bCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // bool algunAprop = false;
        foreach (GameObject obj in objActivacio)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(transform.position, obj.transform.position);
            // Debug.Log("Distancia de " + transform.name + " a " + obj.name + ": " + distance);
            
            if (distance < distanciaMinima)
            {
                ToggleComponents(true);
                break; // Si ja n'hem trobat un, no cal mirar els altres
            }
        }
    }

    void ToggleComponents(bool status)
    {
        // Només canviem l'estat si és diferent de l'actual (estalvia processament)
        if (mRenderer != null) // && mRenderer.enabled != status)
            mRenderer.enabled = status;

        if (bCollider != null) // && bCollider.enabled != status)
            bCollider.enabled = status;
    }
}