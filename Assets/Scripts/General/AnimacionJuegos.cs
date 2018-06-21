//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using ChartboostSDK;

public class AnimacionJuegos : MonoBehaviour
{
    private Vector3 escalaMinimaX;
    private Vector3 escalaMinimaY;

    private Vector3 escalaOptima;

    private bool x;

    void Awake()
    {
        escalaMinimaX = new Vector3(0, 1, 1);
        escalaMinimaY = new Vector3(1, 0, 1);

        escalaOptima = new Vector3(1, 1, 1);
        int i = Random.Range(0, 2);
        if (i == 0)
            x = false;
        else
            x = true;
    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("Aparecer");
    }

    private IEnumerator Aparecer()
    {
        float t = 0f;
        float velAnimacion = 3.75f;

        if (x)
            while (t < 1f)
            {
                t += velAnimacion * Time.deltaTime;
                velAnimacion -= velAnimacion * 0.065f;
                transform.localScale = Vector3.Lerp(escalaMinimaX, escalaOptima, t);

                yield return new WaitForSeconds(0);
            }
        else
            while (t < 1f)
            {
                t += velAnimacion * Time.deltaTime;
                velAnimacion -= velAnimacion * 0.065f;
                transform.localScale = Vector3.Lerp(escalaMinimaY, escalaOptima, t);

                yield return new WaitForSeconds(0);
            }
        x = !x;


        if ((PlayerPrefs.GetInt("publicidad") == 3) && (PlayerPrefs.GetInt("Ads") == 0))
        {
            Chartboost.showInterstitial(CBLocation.HomeScreen);
            PlayerPrefs.SetInt("publicidad", 0);
        }
    }

    private IEnumerator Desaparecer()
    {
        float t = 0f;
        float velAnimacion = 1.75f;

        if (x)
            while (t < 1f)
            {
                t += velAnimacion * Time.deltaTime;
                velAnimacion += velAnimacion * 0.07f;
                transform.localScale = Vector3.Lerp(escalaOptima, escalaMinimaX, t);

                yield return new WaitForSeconds(0);
            }
        else
            while (t < 1f)
            {
                t += velAnimacion * Time.deltaTime;
                velAnimacion += velAnimacion * 0.07f;
                transform.localScale = Vector3.Lerp(escalaOptima, escalaMinimaY, t);

                yield return new WaitForSeconds(0);
            }
        Destroy(gameObject);

        if (Global.modoJuego == "ajustes")
            Global.CrearMenu();
        else
            Global.Puntuaciones();
    }

}
