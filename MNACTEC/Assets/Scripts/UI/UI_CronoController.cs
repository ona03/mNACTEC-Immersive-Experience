using UnityEngine;
using TMPro; // Assegura't d'importar el namespace de TextMeshPro

public class UI_CronoController : MonoBehaviour
{
    public TextMeshProUGUI textTemps; // Arrossega el text del Canvas aquí
    private float tempsTranscorregut = 0f;
    private bool cronometreActiu = false;
    // private float tempsResposta = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cronometreActiu && textTemps.IsActive() == true)
        {
            cronometreActiu = true;
        }
        else if (cronometreActiu && textTemps.IsActive() == false)
        {
            cronometreActiu = false;
            // GuardarTempsResposta();
        }

        if (cronometreActiu)
        {
            // Sumem el temps que ha passat des de l'últim frame
            tempsTranscorregut += Time.deltaTime;
            ActualitzarText();
        }
    }

    void ActualitzarText()
    {
        // Calculem minuts i segons
        int minuts = Mathf.FloorToInt(tempsTranscorregut / 60);
        int segons = Mathf.FloorToInt(tempsTranscorregut % 60);

        // El format "00:00" s'aconsegueix amb string.Format
        textTemps.text = string.Format("{0:00}:{1:00}", minuts, segons);
    }

    public float GetTempsResposta()
    {
        return tempsTranscorregut;
    }
}
