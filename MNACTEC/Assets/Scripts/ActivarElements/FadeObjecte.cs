using UnityEngine;
using System.Collections;

public class FadeObjecte : MonoBehaviour
{
    private MeshRenderer mrenderer;
    [Header("Fade Duration")]
    public float durada = 1.0f;
    [Header("Camins a desactivar (sala 3)")]
    public GameObject path1;
    public GameObject path2; // Assigna aquests camps al inspector
    // public GameObject objectePERactivar;
    // private bool estaVisible = false; // Memòria per saber l'estat actual
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        mrenderer = GetComponent<MeshRenderer>();
        // Opcional: començar l'objecte com a invisible
        // Color c = renderer.material.color;
        // c.a = 1;
        // renderer.material.color = c;
    }

    public void Apareixer()
    {
        // estaVisible = true;
        StopAllCoroutines();
        StartCoroutine(Fade(0, 1));
    }

    public void Desaparèixer()
    {
        // estaVisible = false;
        StopAllCoroutines();
        StartCoroutine(Fade(1, 0));
        // gameObject.SetActive(false);
    }

    IEnumerator Fade(float inici, float final)
    {
        Material mat = mrenderer.material;
        float temps = 0;

        while (temps < durada)
        {
            temps += Time.deltaTime;
            Color c = mat.color;
            // Lerp calcula el pas intermedi entre l'alfa inicial i el final
            c.a = Mathf.Lerp(inici, final, temps / durada);
            mat.color = c;
            yield return null; // Espera al següent frame
        }

        // Ens assegurem que el valor final sigui exacte
        Color finalCol = mat.color;
        finalCol.a = final;
        mat.color = finalCol;
    }

    void ApagarObjecte()
    {
        gameObject.SetActive(false);
    }

    void desactivarCamins()
    {
        path1.SetActive(false);
        path2.SetActive(false);
        // Debug.Log(path1.name + " " + path1.activeSelf);
        // Debug.Log(path2.name + " " + path2.activeSelf);
        // Debug.Log(gameObject.name + " " + gameObject.activeSelf);
    }
}
