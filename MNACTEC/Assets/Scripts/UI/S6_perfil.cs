using UnityEngine;
using TMPro; // Necessari per al component TextMeshProUGUI

public class S6_perfil : MonoBehaviour
{
    private string nom, edat, gènere;
    // private int edat;
    // public Image fons;
    private TextMeshProUGUI dades;
    public MeshRenderer path6; // Assigna el GameObject que conté el script "resumSala1" al Inspector
    private bool textAssginat = false; // Control per assegurar-nos que només assignem el text una vegada
    public GameObject Resum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path6 = GameObject.Find("PathSala6").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (path6.enabled && !textAssginat)
        {
            resumSala1 scriptS1 = Resum.GetComponent<resumSala1>();

            int gènereInt = scriptS1.gènere;
            nom = scriptS1.nom;
            edat = scriptS1.edat.ToString();
            dades = GetComponent<TextMeshProUGUI>();

            // if (dades != null) Debug.Log("TextMeshProUGUI trobat a " + gameObject.name);
            if (dades == null) Debug.LogError("No s'ha trobat el component TextMeshProUGUI a " + gameObject.name);

            gènere = gènereInt == 1 ? "Dona" : gènereInt == 2 ? "Home" : "No binari/Altres";
            
            dades.text = nom + "\n\n" + edat + "\n\n" + gènere;
            
            textAssginat = true; // Assegurem-nos que no tornem a assignar el text en futurs updates
        }
    }
}
