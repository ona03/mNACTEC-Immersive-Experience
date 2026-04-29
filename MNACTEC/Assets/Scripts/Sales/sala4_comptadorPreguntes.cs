using UnityEngine;

public class sala4_comptadorPreguntes : MonoBehaviour
{
    // private GameObject resum4;
    // private GameObject resum;
    private resumSala4 scriptResum; 
    private MeshRenderer mRenderer;
    private BoxCollider bCollider;
    private bool notificat = false; // Per assegurar-nos que només notifiquem un cop per pregunta
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        GameObject resum = GameObject.Find("Resum");
        if (resum != null) scriptResum = resum.GetComponent<resumSala4>();

        mRenderer = GetComponent<MeshRenderer>();
        bCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if (mRenderer != null && mRenderer.enabled && bCollider != null && bCollider.enabled && !notificat)
        {
            // Notifiquem al resum que s'ha activat una pregunta, passant el nom del fill (pregunta)
            if (scriptResum != null)
            {
                scriptResum.NotificarPreguntaActivada(gameObject.name);
                notificat = true; // Evitem notificacions repetides mentre la pregunta segueixi activa
                // Debug.Log($"Notificació enviada a resumSala4 per pregunta: {gameObject.name}");
            }
        }
    }
}
