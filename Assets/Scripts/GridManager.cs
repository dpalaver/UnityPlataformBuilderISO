using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject gridSquarePrefab; // Prefab del cuadrado blanco
    public Vector2 gridSize = new Vector2(100, 100); // Tamaño de la grilla
    private GameObject currentGridSquare; // Cuadrado blanco actual
    private Vector3 currentPosition; // Posición actual del cuadrado blanco

    private void Start()
    {
        currentPosition = Vector3.zero; // Posición inicial
    }

    private void Update()
    {
        UpdateGridPosition();
    }

    private void UpdateGridPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int x = Mathf.FloorToInt(hit.point.x);
            int y = Mathf.FloorToInt(hit.point.z);
            Vector3 newPosition = new Vector3(x, 1.1f, y);

            if (newPosition != currentPosition)
            {
                currentPosition = newPosition;

                if (currentGridSquare != null)
                {
                    Destroy(currentGridSquare);
                }

                currentGridSquare = Instantiate(gridSquarePrefab, currentPosition, Quaternion.identity);
            }
        }
        else
        {
            if (currentGridSquare != null)
            {
                Destroy(currentGridSquare);
                currentGridSquare = null;
            }
        }
    }
}
