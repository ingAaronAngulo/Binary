//Código creado por Aarón Angulo

//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class Transicion : MonoBehaviour
{
    private Transform posicionActual;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    
    private float velAnimacion;

    public Transicion(Transform posicionActual,Vector3 posicionInicial, Vector3 posicionFinal, float velAnimacion)
    {
        this.posicionActual = posicionActual;
        this.posicionInicial = posicionInicial;
        this.posicionFinal = posicionFinal;
        this.velAnimacion = velAnimacion;
    }

    public void RepAdelante()
    {
        StopCoroutine("MoverAtras");
        posicionInicial = posicionActual.position;
        StartCoroutine("MoverAdelante");
    }

    public void RepAtras()
    {
        StopCoroutine("MoverAdelante");
        StartCoroutine("MoverAtras");
    }

    public IEnumerator MoverAdelante()
    {
        float t = 0f;
        while (!posicionActual.localPosition.Equals(posicionFinal))
        {
            t += velAnimacion * Time.deltaTime;
            posicionActual.localPosition = Vector3.Lerp(posicionInicial, posicionFinal, t);
            yield return new WaitForSeconds(0);
        }
    }

    public IEnumerator MoverAtras()
    {
        float t = 0f;
        while (!posicionActual.localPosition.Equals(posicionInicial))
        {
            t += velAnimacion * Time.deltaTime;
            posicionActual.localPosition = Vector3.Lerp(posicionFinal, posicionInicial, t);
            yield return new WaitForSeconds(0);
        }
    }
}
