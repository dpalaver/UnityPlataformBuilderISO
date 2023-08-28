using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool movementEnabled = true; // Variable para controlar el movimiento
    public Vector3 initialPosition; // Variable pública para almacenar la posición inicial
    private float originalMoveSpeed; // Variable para almacenar la velocidad original

    private void Start() {
        initialPosition = transform.position;
        originalMoveSpeed = moveSpeed; // Almacenar la velocidad original al inicio
    }

    private void Update()
    {
        if (movementEnabled)
        {
            // Movimiento isométrico
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
            transform.position += movement;
        }
    }

    public void SetMovementEnabled(bool isEnabled)
    {
        movementEnabled = isEnabled;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void RestoreMoveSpeed()
    {
        moveSpeed = originalMoveSpeed; // Restaurar la velocidad original
    }
}
