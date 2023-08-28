using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f; // Tiempo de suavizado
    public GameManager gameManager;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (gameManager.EstadoJuego == "Construccion")
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

            // Suavizado del movimiento
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
