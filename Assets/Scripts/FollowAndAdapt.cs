using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndAdapt : MonoBehaviour
{
    public Transform objetoAseguir; // Objeto que se seguirá y se adaptará

    private void Update()
    {
        if (objetoAseguir != null)
        {
            // Sigue la posición y el ángulo del objeto a seguir
            Vector3 newPosition = objetoAseguir.position;
            newPosition.y = 1.1f; // Ajusta la posición en el eje Y a 1.1
            transform.position = newPosition;

            transform.rotation = objetoAseguir.rotation;
        }
    }
}