using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private Rigidbody rb;

    private void Start()
    {
        // Obtener el componente Rigidbody del objeto
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        Vector3 subir = GameObject.Find("EjemploSubir").transform.position;
        Vector3 bajar = GameObject.Find("EjemploBajar").transform.position;

        if (collision.gameObject.CompareTag("Subir"))
        {
            bajar.x += 1;
            transform.position = bajar;
        } else if (collision.gameObject.CompareTag("Bajar")) 
        {
            subir.x -= 1;
            transform.position = subir;
        }

        rb.velocity = Vector3.zero;
        
    }

}
