//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class Animaciones : MonoBehaviour
{
    private Vector3 escalaMinima;
    private Vector3 escalaOptima;
    private Vector3 escalaMaxima;
    private float velAnimacion;
    public bool visible;

    void Awake()
    {
        escalaMinima = new Vector3(0, 0, 0);
        escalaOptima = new Vector3(1, 1, 1);
        escalaMaxima = new Vector3(1.15f, 1.15f, 1.15f);
        visible = false;
        transform.localScale = new Vector3();
    }
    
    public void RepAparicion()
    {
        StopCoroutine("Desaparicion");
        StartCoroutine("Aparicion");
    }

    public void RepDesaparicion()
    {
        StopCoroutine("Aparicion");
        StartCoroutine("Desaparicion");
    }

    private IEnumerator Aparicion()
    {
        visible = true;
        float t = 0;
        velAnimacion = 3.75f;

        while(t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            if(velAnimacion > 1.5)
                velAnimacion -= velAnimacion * 0.07f;
            transform.localScale = Vector3.Lerp(escalaMinima, escalaMaxima, t);
            yield return new WaitForSeconds(0);
        }
        
        t = 0.65f;
        velAnimacion = 2f;

        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            if (velAnimacion > 1)
                velAnimacion -= velAnimacion * 0.015f;
            transform.localScale = Vector3.Lerp(escalaMaxima, escalaOptima, t);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator Desaparicion()
    {
        float t = 0;
        velAnimacion = 3.75f;

        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            velAnimacion += velAnimacion * 0.07f;
            transform.localScale = Vector3.Lerp(escalaOptima, escalaMinima, t);

            yield return new WaitForSeconds(0);
        }
        visible = false;

        if (Global.modoJuego == "ajustes")
        {
            Debugger.Escribir("asd");
            Destroy(Global.juego);
            Global.Ajustes();
        }
    }
}