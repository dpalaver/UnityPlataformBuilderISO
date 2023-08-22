using UnityEngine;

public class RemoveRigidbodyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terreno")) // Cambia "Terreno" por la etiqueta del terreno
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Destroy(rigidbody);
            }
        }
    }
}
