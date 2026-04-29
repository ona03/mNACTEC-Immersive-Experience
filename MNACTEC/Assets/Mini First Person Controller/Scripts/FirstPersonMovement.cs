/*using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }
}*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Necessari per al nou sistema

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    // Nota: El nou sistema permet Keyboard.current.leftShiftKey directament

    Rigidbody rb; // 'rigidbody' és una paraula reservada antiga, millor usar 'rb'
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Verifiquem si el teclat està disponible
        if (Keyboard.current == null) return;

        // 1. Detectar si corre (Shift Esquerre)
        IsRunning = canRun && Keyboard.current.leftShiftKey.isPressed;

        // 2. Calcular velocitat objectiu
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // 3. Obtenir input de moviment (WASD / Fleixes)
        float moveX = 0;
        float moveY = 0;

        // Llegim les tecles de direcció del nou sistema
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveY = 1;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveY = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1;
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1;

        Vector2 inputVector = new Vector2(moveX, moveY);
        
        // Normalitzem perquè no camini més ràpid en diagonal
        if (inputVector.magnitude > 1) inputVector.Normalize();

        Vector2 targetVelocity = inputVector * targetMovingSpeed;

        // 4. Aplicar moviment al Rigidbody
        // Nota: He usat rb.linearVelocity per versions recents de Unity o rb.velocity per a les antigues
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 direction = transform.rotation * new Vector3(targetVelocity.x, 0, targetVelocity.y);
        
        rb.linearVelocity = new Vector3(direction.x, currentVelocity.y, direction.z);
    }
}