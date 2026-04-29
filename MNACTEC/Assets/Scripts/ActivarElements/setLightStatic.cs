using UnityEngine;

public class setLightStatic : MonoBehaviour
{
    public float activationDistance = 15f; // Distance at which the light will be activated
    private GameObject player;
    private Light llumComponent;
    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        llumComponent = GetComponent<Light>();
        player = GameObject.FindWithTag("Player"); // Replace "Player" with the actual name of your player object
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (llumComponent != null)
        {
            llumComponent.enabled = distance < activationDistance; // Replace 10f with your desired activation distance
        }
    }
}
