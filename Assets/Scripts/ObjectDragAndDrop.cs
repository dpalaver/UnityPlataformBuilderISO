using UnityEngine;

public class ObjectDragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 targetPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(mainCamera.transform.position.y - transform.position.y)));
            targetPos.y = 1.1f;  // Mantener la posición Y en 1.1f
            transform.position = new Vector3(targetPos.x, 1.1f, targetPos.z);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
    
    private void OnMouseExit()
    {
        if (isDragging)
        {
            // Si el mouse sale del objeto mientras se está arrastrando, detén el arrastre
            isDragging = false;
        }
    }
}
