using UnityEngine;
using TMPro;

public class TextMeshProFading : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private float currentAlpha = 0.4f; // Valor inicial de transparencia
    private float targetAlpha = 1.0f; // Valor de transparencia objetivo

    public float fadeSpeed = 0.5f; // Velocidad de cambio de transparencia

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Cambiar el valor de transparencia de manera ping pong
        currentAlpha = Mathf.PingPong(Time.time * fadeSpeed, 1.0f) * (targetAlpha - 0.4f) + 0.4f;

        // Aplicar la transparencia al componente TextMeshPro
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, currentAlpha);
    }
}
