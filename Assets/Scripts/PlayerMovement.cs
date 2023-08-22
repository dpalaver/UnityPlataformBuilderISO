using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Start()
    {
    }

    private void Update()
    {
        // Movimiento isométrico
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        transform.position += movement;

    }
}
