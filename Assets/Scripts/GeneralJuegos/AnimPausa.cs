//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimPausa : MonoBehaviour
{
    public GameObject resumir;
    public GameObject reiniciar;
    public GameObject salir;
    public GameObject controles;
    public Image panel;
    public Image ocultor;
    public Color alpha;
    public Color solido;

    void Awake()
    {
        panel = GameObject.Find("ControlesPausa").GetComponent<Image>();
        ocultor = GameObject.Find("Ocultor").GetComponent<Image>();
        controles = GameObject.Find("ControlesPausa");
        resumir = GameObject.Find("BtnResumir");
        reiniciar = GameObject.Find("BtnReiniciar");
        salir = GameObject.Find("BtnSalir");
        solido = panel.color;
        alpha = solido;
        solido.a = 1;
        alpha.a = 0;
        controles.SetActive(false);
        ocultor.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    // Use this for initialization
    public void Mostrar()
    {
        controles.SetActive(true);
        StopCoroutine("Desaparecer");
        StartCoroutine("Aparecer");
    }

    public void Ocultar()
    {
        StopCoroutine("Aparecer");
        controles.SetActive(false);
    }

    private IEnumerator Aparecer()
    {
        float t = 0f;
        float velAnimacion = 3.75f;

        ocultor.color = solido;

        resumir.SetActive(false);
        reiniciar.SetActive(false);
        salir.SetActive(false);

        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            velAnimacion += velAnimacion * 0.07f;
            panel.color = Color.Lerp(alpha, solido, t);

            yield return new WaitForSeconds(0);
        }

        ocultor.gameObject.SetActive(true);
        resumir.SetActive(true);
        reiniciar.SetActive(true);
        salir.SetActive(true);
        t = 0f;
        velAnimacion = 3.75f;

        while (t < 1f)
        {
            t += velAnimacion * Time.deltaTime;
            velAnimacion += velAnimacion * 0.07f;
            ocultor.color = Color.Lerp(solido, alpha, t);

            yield return new WaitForSeconds(0);
        }
        ocultor.gameObject.SetActive(false);
    }
}
