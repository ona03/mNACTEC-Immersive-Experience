using UnityEngine;
using System.Collections;

public class particlePath : MonoBehaviour
{
    private Animator particleAnimator;
    public int tempsEspera = 2; // Temps en segons abans d'activar la nova etapa
    private AudioSource etapaPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particleAnimator = GetComponent<Animator>();
        etapaPath = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Trigger activat per: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            StartCoroutine(NovaEtapa());
        }
    }

    IEnumerator NovaEtapa()
    {
        yield return new WaitForSeconds(tempsEspera); // Espera 1 segon abans d'activar la nova etapa
        particleAnimator.SetTrigger("nextStep");
        etapaPath.Play(0);
    }
}
