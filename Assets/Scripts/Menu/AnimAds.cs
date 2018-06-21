//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class AnimAds : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    public bool precios;
    private float velAnimacion;

    void Awake()
    {
        posicionInicial = transform.localPosition;
        posicionFinal = new Vector3(0, -175, 0);
        precios = false;
    }

    public void Precios()
    {
        if (precios)
        {
            precios = false;
            StopCoroutine("Subir");
            StartCoroutine("Bajar");
        }
        else
        {
            StopCoroutine("Bajar");
            StartCoroutine("Subir");
            precios = true;
        }
    }

    public IEnumerator Subir()
    {
        float t = 0f;
        velAnimacion = 4;

        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            if (velAnimacion > 1)
                velAnimacion -= velAnimacion * 0.065f;
            transform.localPosition = Vector3.Lerp(posicionInicial, posicionFinal, t);
            yield return new WaitForSeconds(0);
        }
    }

    public IEnumerator Bajar()
    {
        float t = 0f;
        velAnimacion = 4;

        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            if (velAnimacion > 1)
                velAnimacion -= velAnimacion * 0.065f;
            transform.localPosition = Vector3.Lerp(posicionFinal, posicionInicial, t);
            yield return new WaitForSeconds(0);
        }
    }
}
