using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnAtCoordinate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject prefabToCreate;
    public GameObject nuevoPrefab;
    public string cuadradoGrillaTag = "CuadradoGrilla";
    public GameManager gameManager;

    private GameObject instantiatedPrefab;
    private GameObject newInstantiatedPrefab;
    private bool isButtonDown = false;
    private GameObject lastCuadradoGrilla;
    private float currentAngle = 0.0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelCreation();
        }

        if (isButtonDown && instantiatedPrefab != null && lastCuadradoGrilla != null)
        {
            if (prefabToCreate.name == "EscaleraPlano")
            {
                instantiatedPrefab.transform.position = lastCuadradoGrilla.transform.position + new Vector3(1.0f, 0.0f, 0.9f);
            }
            else
            {
                instantiatedPrefab.transform.position = lastCuadradoGrilla.transform.position;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                currentAngle = (currentAngle == 0.0f) ? 90.0f : 0.0f;
                instantiatedPrefab.transform.rotation = Quaternion.Euler(Vector3.up * currentAngle);
            }
        }
    }
    private void CancelCreation()
    {
        if (isButtonDown)
        {
            if (instantiatedPrefab != null)
            {
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }

            gameManager.SetGameObjectVisibility("UIMover",false);
            isButtonDown = false;

        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        gameManager.SetGameObjectVisibility("UIMover",true);

        if (nuevoPrefab.name == "Tabla") {

            int tablas = gameManager.TablasCantidad;

            if (tablas >= 1) {
                isButtonDown = true;
                instantiatedPrefab = Instantiate(prefabToCreate);
            } else {

                Debug.LogWarning("No hay cantidad suficiente de " + nuevoPrefab.name + "s");
                gameManager.StartBlinking("TextoBtn1", 0.6f, 0.2f);
                gameManager.SetGameObjectVisibility("UIMover",false);

            }

        } else if (nuevoPrefab.name == "Andamio") {

            int andamios = gameManager.AndamiosCantidad;

            if (andamios >= 1) {
                isButtonDown = true;
                instantiatedPrefab = Instantiate(prefabToCreate);
            } else {

                Debug.LogWarning("No hay cantidad suficiente de " + nuevoPrefab.name + "s");
                gameManager.StartBlinking("TextoBtn2", 0.6f, 0.2f);
                gameManager.SetGameObjectVisibility("UIMover",false);

            }

        } else if (nuevoPrefab.name == "Escalera") {

            int escalera = gameManager.EscalerasCantidad;

            if (escalera >= 1) {
                isButtonDown = true;
                instantiatedPrefab = Instantiate(prefabToCreate);
            } else {

                Debug.LogWarning("No hay cantidad suficiente de " + nuevoPrefab.name + "s");
                gameManager.StartBlinking("TextoBtn3", 0.6f, 0.2f);
                gameManager.SetGameObjectVisibility("UIMover",false);

            }

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameManager.SetGameObjectVisibility("UIMover", false);

        // Verificar si el puntero está sobre objetos con los nombres "btn1", "btn2" o "btn3"
        if (eventData.pointerEnter != null)
        {
            string objectName = eventData.pointerEnter.gameObject.name;
            if (objectName == "btn1" || objectName == "btn2" || objectName == "btn3")
            {
                CancelCreation(); // Llamamos a la función CancelCreation para limpiar
                return; // No se permite soltar el mouse si está sobre uno de estos objetos
            }
        }

        if (nuevoPrefab.name == "Tabla") {

            int tablas = gameManager.TablasCantidad;

            if (tablas >= 1) {

                isButtonDown = false;

                if (instantiatedPrefab != null) {

                    Vector3 newPosition = instantiatedPrefab.transform.position + new Vector3(0f, 10f, 0f);
                    newInstantiatedPrefab = Instantiate(nuevoPrefab, newPosition, instantiatedPrefab.transform.rotation);
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;

                    gameManager.ModificarVariable(nuevoPrefab.name,1,"restar");

                }

            } else {

                Debug.LogWarning("No hay cantidad suficiente de " + nuevoPrefab.name + "s");

            }

        } else if (nuevoPrefab.name == "Andamio") {

            int andamios = gameManager.AndamiosCantidad;

            if (andamios >= 1) {

                isButtonDown = false;

                if (instantiatedPrefab != null) {

                    Vector3 newPosition = instantiatedPrefab.transform.position + new Vector3(0f, 10f, 0f);
                    newInstantiatedPrefab = Instantiate(nuevoPrefab, newPosition, instantiatedPrefab.transform.rotation);
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;

                    gameManager.ModificarVariable(nuevoPrefab.name,1,"restar");

                }

            } else {

                Debug.LogWarning("No hay cantidad suficiente de " + nuevoPrefab.name + "s");

            }

        } else if (nuevoPrefab.name == "Escalera") {

            int escalera = gameManager.EscalerasCantidad;

            if (escalera >= 1) {

                isButtonDown = false;

                if (instantiatedPrefab != null) {

                    Vector3 newPosition = instantiatedPrefab.transform.position + new Vector3(0f, 0f, 0f);
                    newInstantiatedPrefab = Instantiate(nuevoPrefab, newPosition, instantiatedPrefab.transform.rotation);
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;

                    gameManager.ModificarVariable(nuevoPrefab.name,1,"restar");

                }

            } else {

                Debug.LogWarning("No hay cantidad suficiente de " + nuevoPrefab.name + "s");

            }

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