using UnityEngine;

public class obrirPortes : MonoBehaviour
{
    Animator animator;
    GameObject jugador;
    private float distance;
    private float distanciaMinima = 10f; // La distància mínima per activar el trigger
    private AudioSource slidingDoor;
    //private bool jugadorAprop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        animator.SetBool("jugadorAprop", false);
        slidingDoor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador == null) return;

        distance = Vector3.Distance(transform.position, jugador.transform.position);
        
        if (distance <= distanciaMinima && animator.GetBool("jugadorAprop") == false)
        {
            animator.SetBool("jugadorAprop", true);
            slidingDoor.Play(0);
        }
        else if (distance >= distanciaMinima && animator.GetBool("jugadorAprop") == true)
        {
            animator.SetBool("jugadorAprop", false);
            slidingDoor.Play(0);
        }
    }
}
