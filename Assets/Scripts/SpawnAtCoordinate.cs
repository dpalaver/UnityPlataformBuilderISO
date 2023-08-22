using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnAtCoordinate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject prefabToCreate; // Asigna el prefab en el inspector
    public GameObject nuevoPrefab; // Asigna el nuevo prefab en el inspector
    public string cuadradoGrillaTag = "CuadradoGrilla"; // Tag del objeto "CuadradoGrilla"

    private GameObject instantiatedPrefab;
    private GameObject newInstantiatedPrefab; // Variable para el nuevo prefab
    private bool isButtonDown = false;
    private GameObject lastCuadradoGrilla;

    private void Update()
    {
        if (isButtonDown && instantiatedPrefab != null && lastCuadradoGrilla != null)
        {
            instantiatedPrefab.transform.position = lastCuadradoGrilla.transform.position;

            if (Input.GetKeyDown(KeyCode.R))
            {
                instantiatedPrefab.transform.Rotate(Vector3.up, 90.0f);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonDown = true;
        instantiatedPrefab = Instantiate(prefabToCreate);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
        if (instantiatedPrefab != null)
        {
            Vector3 newPosition = instantiatedPrefab.transform.position + new Vector3(0f, 10f, 0f);
            newInstantiatedPrefab = Instantiate(nuevoPrefab, newPosition, instantiatedPrefab.transform.rotation);
            Destroy(instantiatedPrefab);
            instantiatedPrefab = null;
        }
    }

    private void LateUpdate()
    {
        GameObject[] cuadrosGrilla = GameObject.FindGameObjectsWithTag(cuadradoGrillaTag);
        if (cuadrosGrilla.Length > 0)
        {
            lastCuadradoGrilla = cuadrosGrilla[cuadrosGrilla.Length - 1];
        }
        else
        {
            lastCuadradoGrilla = null;
        }
    }
}