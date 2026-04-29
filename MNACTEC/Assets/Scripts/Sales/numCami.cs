using UnityEngine;

public class numCami : StateMachineBehaviour
{
    // private int opcio;
    private GameObject pareCamins; // Assigna aquest camp al inspector o deixa-ho buit per usar aquest mateix objecte
    // public GameObject alternativaCamí1, alternativaCamí2; // Assigna aquests camps al inspector
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Busquem l'objecte amb el Tag en el moment de sortir (ja que no hi ha Start)
        GameObject pareCamins = GameObject.FindWithTag("pareCamins");

        if (pareCamins != null)
        {
            numPath scriptCamins = pareCamins.GetComponent<numPath>();

            if (scriptCamins != null)
            {
                // Fem servir 'animator.name' per saber quin objecte ha acabat l'animació
                if (animator.name == "PathSala3_N")
                {
                    scriptCamins.SetOpcio(1);
                    // Debug.Log("Opció N assignada");
                }
                else if (animator.name == "PathSala3_F")
                {
                    scriptCamins.SetOpcio(2);
                    // Debug.Log("Opció F assignada");
                }
                else if (animator.name == "PathSala3_M")
                {
                    scriptCamins.SetOpcio(3);
                    // Debug.Log("Opció M assignada");
                }
            }
        }
        else
        {
            Debug.LogError("No s'ha trobat cap objecte amb el Tag 'pareCamins'!");
        }
        // Debug.Log("Animació acabada en l'objecte: " + animator.name);

        // Animator anim1 = alternativaCamí1.GetComponent<Animator>();
        // Animator anim2 = alternativaCamí2.GetComponent<Animator>();

        // anim1.SetBool("camiJaTriat", true);
        // anim2.SetBool("camiJaTriat", true);
    }
}
