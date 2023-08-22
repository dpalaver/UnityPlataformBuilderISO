using UnityEngine;

public class StopOnCollision : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private bool isColliding = false;
    private float bounceForce = 5f; // Fuerza del rebote

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (isColliding)
        {
            // Si está colisionando, desactiva los inputs del jugador
            playerMovement.enabled = false;
        }
        else
        {
            // Si no está colisionando, activa los inputs del jugador
            playerMovement.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Decoracion"))
        {
            // Aplicar rebote hacia atrás y detener el movimiento
            Vector3 direction = (transform.position - collision.contacts[0].point).normalized;
            rb.velocity = direction * bounceForce;

            // Desactivar al jugador temporalmente para evitar rebotes múltiples
            rb.isKinematic = true;

            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Decoracion"))
        {
            // Reactivar el Rigidbody y permitir los inputs al salir de la colisión
            rb.isKinematic = false;
            isColliding = false;
        }
    }
}
