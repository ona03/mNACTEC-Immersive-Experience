using UnityEngine;

public class floatingObjRaycast : MonoBehaviour
{
    // position Y
    private float startYPos;
    private float heightSpeed = .8f;
    private float heightAmplitude = .1f;
    private float angleForHeigh = 0;
    
    private bool sentApuntat = false;
    private float tempsSentApuntat = 0f;

    [Header("Configuració de Grup")]
    public string nomGrup; // per sala 5, tipus d'imatge

    void Start()
    {
        startYPos = transform.position.y;
    }

    void Update()
    {
        if (sentApuntat)
        {
            //Debug.Log("Objecte " + gameObject.name + " està sent mirat. Activant moviment.");
            angleForHeigh += heightSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, startYPos + Mathf.Sin(angleForHeigh) * heightAmplitude, transform.position.z);
            if (gameObject.tag == "panell sala5")
            {
                // Si és un panell de la sala 5, el temps que està sent apuntat es comptabilitza
                tempsSentApuntat += Time.deltaTime;
            }
        }

        // Es reseteja el booleà: si el Raycast no el torna a posar a true al següent frame, s'aturarà
        sentApuntat = false;
        //Debug.Log("Objecte " + gameObject.name + " no està sent mirat. Desactivant moviment.");
    }

    public void ActivarMovimentPerAquestFrame()
    {
        sentApuntat = true;
    }

    public float GetTempsSentApuntat()
    {
        return tempsSentApuntat;
    }
}