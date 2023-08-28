using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // Amplitud de la oscilación
    public float frequency = 1f; // Frecuencia de la oscilación

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calcular la posición vertical oscilante
        Vector3 newPosition = initialPosition + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;

        // Aplicar la nueva posición al GameObject de texto
        transform.position = newPosition;
    }
}
