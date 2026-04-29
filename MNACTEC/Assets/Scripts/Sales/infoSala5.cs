using UnityEngine;

public class infoSala5 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject resum = GameObject.FindWithTag("resumUsuari");

        if (resum != null)
        {
            resum.GetComponent<resumSala5>().ProcessarDadesSala5();
            // Debug.Log("Dades de la sala 5 processades i resum actualitzat.");
        }
        
    }
}
