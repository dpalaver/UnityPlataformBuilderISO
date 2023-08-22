using UnityEngine;

public class ReturnToOrigin : MonoBehaviour
{
    private Vector3 originalPosition; // Punto de origen del objeto

    private void Start()
    {
        originalPosition = transform.position; // Guardar el punto de origen del objeto al iniciar
    }

    private void Update()
    {
        // Verificar posición en eje Y y volver al punto de origen si es menor que 1
        if (transform.position.y < 1f)
        {
            transform.position = originalPosition;
        }
    }
}
