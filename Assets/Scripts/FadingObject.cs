using System.Collections;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    public float cycleDuration = 1f; // Duración del ciclo en segundos
    public float minAlpha = 0.4f;    // Valor mínimo de transparencia (40%)
    public float maxAlpha = 1f;      // Valor máximo de transparencia (100%)
    public AnimationCurve curve;     // Curva de interpolación

    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        StartCoroutine(CycleTransparency());
    }

    private IEnumerator CycleTransparency()
    {
        while (true)
        {
            float elapsedTime = 0f;

            while (elapsedTime < cycleDuration)
            {
                // Calcula el valor de transparencia en función de la curva de interpolación
                float lerpValue = curve.Evaluate(elapsedTime / cycleDuration);
                float currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, lerpValue);

                // Aplica el nuevo valor de transparencia al material
                Color newColor = new Color(material.color.r, material.color.g, material.color.b, currentAlpha);
                material.color = newColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Al final del ciclo, invierte los valores para hacer que la transparencia oscile
            float temp = minAlpha;
            minAlpha = maxAlpha;
            maxAlpha = temp;

            yield return new WaitForSeconds(1f); // Esperar 1 segundo antes de comenzar el próximo ciclo
        }
    }
}
