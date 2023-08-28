using UnityEngine;
using UnityEngine.UI;

public class RecargaConSprite : MonoBehaviour
{
    public float tiempoTotalRecarga = 5.0f; // Tiempo total de recarga
    public float tiempoRestanteRecarga = 0.0f; // Tiempo restante de recarga
    public GameObject barraRecargaFill; // Objeto que actúa como el relleno de la barra de recarga
    private Image fillImage; // Componente Image del relleno de la barra de recarga

    private void Start()
    {
        tiempoRestanteRecarga = tiempoTotalRecarga; // Iniciar tiempo restante con valor máximo
        fillImage = barraRecargaFill.GetComponent<Image>(); // Obtener el componente Image del relleno
    }

    private void Update()
    {
        if (tiempoRestanteRecarga > 0)
        {
            tiempoRestanteRecarga -= Time.deltaTime;
            float fillAmount = tiempoRestanteRecarga / tiempoTotalRecarga;
            fillImage.fillAmount = fillAmount; // Actualizar la escala del relleno según el progreso
        }
    }
}
