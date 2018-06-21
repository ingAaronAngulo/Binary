//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialConversor : MonoBehaviour
{
    private Vector3 posicionActual;
    private Vector3 posicionProxima;
    private float velAnimacion;

    private string[] textos = new string[4];

    void Awake()
    {
        textos = new Dialogos().TutorialConversor();
        GameObject.Find("TxtConversor0").GetComponent<Text>().text = textos[0];
        GameObject.Find("TxtConversor1").GetComponent<Text>().text = textos[1];
        GameObject.Find("TxtSiguiente").GetComponent<Text>().text = textos[2];
        GameObject.Find("TxtCerrar").GetComponent<Text>().text = textos[3];

        velAnimacion = 1.75f;
    }

    void Start()
    {
        posicionActual = transform.localPosition;
        posicionProxima = new Vector3();
    }

    public void Siguiente()
    {
        StopCoroutine("Animacion");
        posicionActual = transform.localPosition;
        posicionProxima.Set(posicionActual.x - 800, posicionActual.y, posicionActual.z);
        StartCoroutine("Animacion");
    }

    public IEnumerator Animacion()
    {
        float t = 0f;
        while (!transform.position.Equals(posicionProxima))
        {
            t += velAnimacion * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(posicionActual, posicionProxima, t);
            yield return new WaitForSeconds(0);
        }
    }

    public void Cerrar()
    {
        Destroy(gameObject);
    }
}
