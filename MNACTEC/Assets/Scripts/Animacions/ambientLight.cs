using UnityEngine;
using System.Collections;

public class ambientLight : MonoBehaviour
{
    // Arrays fixos de 6 elements
    public GameObject[] paths = new GameObject[6];
    private MeshRenderer[] renderers = new MeshRenderer[6];
    private bool[] pathsProcessats = new bool[6];

    private Light llumComponent;

    void Start()
    {
        llumComponent = GetComponent<Light>();
        llumComponent.intensity = 3;
        // RenderSettings.ambientIntensity = 0.5f; 
        // rotació inici (65, 142, 212)

        // Guardem els renderers al principi per optimitzar
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i] != null)
            {
                renderers[i] = paths[i].GetComponent<MeshRenderer>();
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            // Comprovem si el path existeix, si és el primer cop que s'activa i si el seu renderer està actiu
            if (paths[i] != null && !pathsProcessats[i] && renderers[i].enabled)
            {
                AplicarCanvi(i);
                pathsProcessats[i] = true;
            }
        }
    }

    void AplicarCanvi(int index)
    {
        Quaternion desti;
        switch (index)
        {
            case 0: // Path 1
                StartCoroutine(CanviarIntensitatProgressiva(3f, 2f, 0.5f));
                break;
            case 1: // Path 2
                desti = Quaternion.Euler(55, 135, 237);
                StartCoroutine(CanviarRotacioProgressiva(transform.rotation, desti, 10f));
                break;
            case 2: // Path 3
                // StartCoroutine(CanviarIntensitatProgressiva(2f, 1f, 1f));
                desti = Quaternion.Euler(65, 225, 230);
                StartCoroutine(CanviarRotacioProgressiva(transform.rotation, desti, 5f));
                break;
            case 3: // Path 4
                RenderSettings.ambientIntensity = 0.5f;
                StartCoroutine(CanviarIntensitatProgressiva(2f, 0.5f, 3f));
                desti = Quaternion.Euler(0, 100, 225);
                StartCoroutine(CanviarRotacioProgressiva(transform.rotation, desti, 3f));
                break;
            case 4: // Path 5
                StartCoroutine(CanviarIntensitatProgressiva(0.5f, 0.1f, 1f));
                break;
            case 5:
                RenderSettings.ambientIntensity = 0.1f; 
                break;
            default:
                break;
        }
    }
    IEnumerator CanviarIntensitatProgressiva(float valorInicial, float valorFinal, float durada)
    {
        float tempsTranscorregut = 0;

        while (tempsTranscorregut < durada)
        {
            // Calculem el progrés (de 0 a 1)
            float progressio = tempsTranscorregut / durada;
            
            // Apliquem la interpolació lineal
            llumComponent.intensity = Mathf.Lerp(valorInicial, valorFinal, progressio);
            
            tempsTranscorregut += Time.deltaTime;

            // DEBUG DE CONTROL (Mateix format)
            // Debug.Log($"VALORS INTENSITAT: " +
            //           $"Intensitat Actual: {llumComponent.intensity} | " +
            //           $"Progrés: {progressio * 100}% | " +
            //           $"Temps: {tempsTranscorregut}");

            yield return null; // Espera al següent frame
        }

        // Ens assegurem que quedi exactament al valor final
        llumComponent.intensity = valorFinal;
    }
    IEnumerator CanviarRotacioProgressiva(Quaternion rotacioInicial, Quaternion rotacioFinal, float durada)
    {
        float tempsTranscorregut = 0;

        while (tempsTranscorregut < durada)
        {
            float progressio = tempsTranscorregut / durada;

            // Apliquem la rotació progressiva
            transform.rotation = Quaternion.Slerp(rotacioInicial, rotacioFinal, progressio);

            tempsTranscorregut += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rotacioFinal;
    }
}
