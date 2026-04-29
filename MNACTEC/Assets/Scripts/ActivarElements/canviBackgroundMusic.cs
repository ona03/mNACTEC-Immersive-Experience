using UnityEngine;
using System.Collections;

public class canviBackgroundMusic : MonoBehaviour
{
    private AudioSource audioFons;
    public AudioClip[] musica = new AudioClip[4];
    public GameObject[] paths = new GameObject[4];
    private MeshRenderer[] renderers = new MeshRenderer[4];
    private bool[] pathsProcessats = new bool[4];
    // public float tempsTransicio = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioFons = this.GetComponent<AudioSource>();
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i] != null)
            {
                renderers[i] = paths[i].GetComponent<MeshRenderer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            // Comprovem si el path existeix, si és el primer cop que s'activa i si el seu renderer està actiu
            if (paths[i] != null && !pathsProcessats[i] && renderers[i].enabled)
            {
                if (i == 3) audioFons.volume = 1;
                IniciarCanvi(musica[i]);
                pathsProcessats[i] = true;
            }
        }
    }
    public void IniciarCanvi(AudioClip musicaNova)
    {
        StartCoroutine(TransicioMusica(musicaNova));
    }

    IEnumerator TransicioMusica(AudioClip musicaNova)
    {
        float tempsTransicio = 1.5f;
        float volumInicial = audioFons.volume;

        // 1. Fade Out (Baixar volum)
        for (float t = 0; t < tempsTransicio; t += Time.deltaTime)
        {
            audioFons.volume = Mathf.Lerp(volumInicial, 0, t / tempsTransicio);
            yield return null;
        }

        audioFons.volume = 0;
        audioFons.Stop();

        // 2. Canviar Clip i Play
        audioFons.clip = musicaNova;
        audioFons.Play();

        // 3. Fade In (Pujar volum)
        for (float t = 0; t < tempsTransicio; t += Time.deltaTime)
        {
            audioFons.volume = Mathf.Lerp(0, volumInicial, t / tempsTransicio);
            yield return null;
        }

        audioFons.volume = volumInicial;
    }
}
