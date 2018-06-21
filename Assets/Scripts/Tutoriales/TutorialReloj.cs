//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialReloj : MonoBehaviour
{
    private Vector3 posicionActual;
    private Vector3 posicionProxima;
    private float velAnimacion;

    private string[] textos = new string[6];

	void Awake()
    {
        textos = new Dialogos().TutorialReloj();
        for (int i = 0; i < 4; i++)
            GameObject.Find("TxtReloj" + i).GetComponent<Text>().text = textos[i];
        GameObject.Find("Siguiente1").GetComponent<Text>().text = textos[4];
        GameObject.Find("Siguiente2").GetComponent<Text>().text = textos[4];
        GameObject.Find("Cerrar1").GetComponent<Text>().text = textos[5];

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
            transform.localPosition = Vector3.Lerp(posicionActual , posicionProxima, t);
            yield return new WaitForSeconds(0);
        }
    }

    public void Cerrar()
    {
        Destroy(gameObject);
    }
}
