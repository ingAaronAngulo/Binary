//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class AnimPuntuacion : MonoBehaviour
{
    private Transform puntajes;
    private Transform reiniciar;
    private Transform salir;

    //posicion afuera del boton Reiniciar
    private Vector3 pFueraReiniciar;
    private Vector3 pOptimaReiniciar;
    private Vector3 pDesfasadaReiniciar;
    //posicion afuera del boton Salir
    private Vector3 pFueraSalir;
    private Vector3 pDesfasadaSalir;
    private Vector3 pOptimaSalir;
    //escalas de los puntajes
    private Vector3 escalaMinima;
    private Vector3 escalaOptima;
    private Vector3 escalaMaxima;

    void Awake()
    {
        puntajes = transform.Find("Puntajes");

        reiniciar = GameObject.Find("Reiniciar").GetComponent<Transform>();
        salir = GameObject.Find("Salir").GetComponent<Transform>();
        escalaMinima = new Vector3();
        escalaOptima = new Vector3(1, 1, 1);
        escalaMaxima = new Vector3(1.15f, 1.15f, 1.15f);
        pOptimaReiniciar = reiniciar.localPosition;
        pOptimaSalir = salir.localPosition;
    }

    public void Iniciar()
    {
        puntajes.localScale = escalaMinima;
        pFueraReiniciar = new Vector3(pOptimaReiniciar.x - 550, pOptimaReiniciar.y, pOptimaReiniciar.y);
        pFueraSalir = new Vector3(pOptimaSalir.x + 550, pOptimaSalir.y, pOptimaSalir.y);

        pDesfasadaReiniciar = new Vector3(pOptimaReiniciar.x + 100, pOptimaReiniciar.y, pOptimaReiniciar.z);
        pDesfasadaSalir = new Vector3(pOptimaSalir.x - 100, pOptimaSalir.y, pOptimaSalir.z);

        reiniciar.localPosition = pFueraReiniciar;
        salir.localPosition = pFueraSalir;

        StartCoroutine("Aparicion");
    }

    public void Destruir(byte opcion)
    {
        StopCoroutine("Aparicion");
        StartCoroutine("DesAparicion", opcion);
    }

    private IEnumerator Aparicion()
    {
        float t = 0f;
        float velAnimacion = 3.75f;
        
        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            velAnimacion -= velAnimacion * 0.035f;
            puntajes.localScale = Vector3.Lerp(escalaMinima, escalaMaxima, t);
            reiniciar.localPosition = Vector3.Lerp(pFueraReiniciar, pDesfasadaReiniciar, t);
            salir.localPosition = Vector3.Lerp(pFueraSalir, pDesfasadaSalir, t);
            yield return new WaitForSeconds(0);
        }

        t = 0.60f;
        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            velAnimacion += velAnimacion * 0.025f;
            puntajes.localScale = Vector3.Lerp(escalaMaxima, escalaOptima, t);
            reiniciar.localPosition = Vector3.Lerp(pDesfasadaReiniciar, pOptimaReiniciar, t);
            salir.localPosition = Vector3.Lerp(pDesfasadaSalir, pOptimaSalir, t);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator DesAparicion(byte opcion)
    {
        float t = 0f;
        float velAnimacion = 1.55f;
        
        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            velAnimacion += velAnimacion * 0.065f;
            puntajes.localScale = Vector3.Lerp(escalaOptima, escalaMinima, t);
            reiniciar.localPosition = Vector3.Lerp(pOptimaReiniciar, pFueraReiniciar, t);
            salir.localPosition = Vector3.Lerp(pOptimaSalir, pFueraSalir, t);
            yield return new WaitForSeconds(0);
        }
        if (opcion == 0)
        {
            Global.CrearMenu();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Global.Reiniciar();
        }
    }
}