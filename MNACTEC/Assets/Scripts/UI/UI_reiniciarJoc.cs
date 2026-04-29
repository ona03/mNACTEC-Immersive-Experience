using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_reiniciarJoc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Reiniciar()
    {
        // Obtenim l'índex de l'escena actual i la tornem a carregar
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);

        // Opcional: Si el joc estava pausat (Time.timeScale = 0), el tornem a la normalitat
        Time.timeScale = 1f;

        // Debug.Log("Joc Reiniciat: Escena " + escenaActual);
    }
    public void SortirJoc()
    {
        Application.Quit();
        Debug.Log("Sortint del joc...");
    }
}
