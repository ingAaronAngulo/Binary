//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class Fondo : MonoBehaviour
{
    private SpriteRenderer renderer;
    private float t;
    private float velAnimacion;

    private Color actual;
    public Color[] colores = new Color[5];
    /*
        [0] = menu;
        [1] = reloj;
        [2] = sumatoria;
        [3] = conversor;
        [4] = ajustes;
    */

    // Use this for initialization
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        velAnimacion = 1f;
        /*
        colores[0] = new Color(238.0f / 255, 238.0f / 255, 238.0f / 255);
        colores[1] = new Color(255.0f / 255, 249.0f / 255, 201.0f / 255);
        colores[2] = new Color(210.0f / 255, 244.0f / 255, 255.0f / 255);
        colores[3] = new Color(255.0f / 255, 197.0f / 255, 212.0f / 255);
        colores[4] = new Color(195.0f / 255, 255.0f / 255, 195.0f / 255);*/
    }

    void Start()
    {
        actual = colores[0];
        renderer.color = actual;
    }

    public void RepCambioColor(int indice)
    {
        StopCoroutine("CambiarColor");
        actual = renderer.color;
        StartCoroutine("CambiarColor",indice);
    }

    public IEnumerator CambiarColor(int indice)
    {
        t = 0;
        while(!renderer.color.Equals(colores[indice]))
        {
            t += velAnimacion * Time.deltaTime;
            renderer.color = Color.Lerp(actual, colores[indice], t);

            yield return new WaitForSeconds(0);
        }
    }
}