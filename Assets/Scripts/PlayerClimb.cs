using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 15.0f; // Velocidad de escalada

    private bool Escalando = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            Debug.Log("Colisión con: " + collision.gameObject.name);
            Escalando = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Escalera"))
        {
            Escalando = false;
        }
    }

    private void Update()
    {
        if (Escalando)
        {
            float newY = transform.position.y + climbSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
