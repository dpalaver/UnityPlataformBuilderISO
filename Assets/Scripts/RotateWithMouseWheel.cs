using UnityEngine;

public class RotateWithMouseWheel : MonoBehaviour
{
    public string targetTag = "objeto"; // Tag del objeto que deseas rotar

    private void Update()
    {
        // Lanzar un rayo desde la posición del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(targetTag))
        {
            // Verificar si el objeto apuntado tiene el componente Transform
            Transform hitTransform = hit.transform;
            if (hitTransform != null)
            {
                // Verificar si se presionó la tecla "r"
                if (Input.GetKeyDown(KeyCode.R))
                {
                    // Calcula la cantidad de rotación en grados (multiplicada por 90 para pasos de 90 grados)
                    float rotationAmount = 90f;

                    // Aplica la rotación al objeto apuntado en pasos de 90 grados
                    hitTransform.Rotate(Vector3.up, rotationAmount);
                }
            }
        }
    }
}
