using UnityEngine;

public class resumSala1 : MonoBehaviour
{
    private GameObject path1;
    [Header("SALA 1")]
    // public GameObject path1;
    public string nom = ""; 
    public int edat = 0, gènere = 0; 

    private UI_InputFieldController inputNom;
    private UI_InputFieldController inputEdat;
    private UI_ButtonController ctrlGenere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject infoSala1 = GameObject.Find("canvasSala1");
        path1 = GameObject.Find("PathSala1");

        if (infoSala1 != null)
        {
            // Busquem i guardem les referències UNA SOLA VEGADA
            Transform tNom = infoSala1.transform.Find("Nom");
            if (tNom != null) inputNom = tNom.GetComponent<UI_InputFieldController>();

            Transform tEdat = infoSala1.transform.Find("Edat");
            if (tEdat != null) inputEdat = tEdat.GetComponent<UI_InputFieldController>();

            Transform tGen = infoSala1.transform.Find("Gènere/botons");
            if (tGen != null) ctrlGenere = tGen.GetComponent<UI_ButtonController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path1 == null || !path1.activeSelf) return;

        if (path1.gameObject.activeSelf)
        {
            // 1. GESTIÓ DEL NOM (Només si encara no tenim el nom)
            if (string.IsNullOrEmpty(nom) && inputNom != null)
            {
                string textDetectat = inputNom.GetTextFinal();
                if (!string.IsNullOrWhiteSpace(textDetectat))
                {
                    nom = textDetectat;
                    // Debug.Log($"Nom guardat i bloquejat: {nom}");
                }
            }

            // 2. GESTIÓ DE L'EDAT (Només si edat és 0)
            if (edat == 0 && inputEdat != null)
            {
                string textDetectat = inputEdat.GetTextFinal();
                if (int.TryParse(textDetectat, out int edatDetectada) && edatDetectada > 0)
                {
                    edat = edatDetectada;
                    // Debug.Log($"Edat guardada i bloquejada: {edat}");
                }
            }

            // 3. GESTIÓ DEL GÈNERE (Només si gènere és 0)
            if (gènere == 0 && ctrlGenere != null)
            {
                int tria = ctrlGenere.GetOpcioTriada();
                if (tria != 0)
                {
                    gènere = tria;
                    // Debug.Log($"Gènere guardat: {gènere}");
                }
            }
        }
        
    }
}
