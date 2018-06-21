//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage 
 *  Maneja la lógica en el modo de juego Conversor
 * 
 */
public class Conversion : MonoBehaviour 
{
	private GameObject decimalGo;
	private GameObject respuestaGo;
	private Text decimalTxt;
	private Text respuestaTxt;
    private Text[] binariosTxt = new Text[8];
    private Text puntuacionTxt;
    private NumeroBin[] binarios = new NumeroBin[8];
    private Temporizador temporizador;
    private int puntuacion;

    /**
    *   Se encuentran GameObjects y se inicializan variables
    */
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        decimalGo = GameObject.Find("decimalTxt").gameObject;
        respuestaGo = GameObject.Find("respuestaTxt").gameObject;
        respuestaTxt = respuestaGo.GetComponent<Text>();
        decimalTxt = decimalGo.GetComponent<Text>();
        puntuacionTxt = GameObject.Find("Puntuacion").GetComponent<Text>();

        for (int i = 0; i < binarios.Length; i++)
        {
            binarios[i] = GameObject.Find("" + i).GetComponent<NumeroBin>();
            binariosTxt[i] = GameObject.Find("" + i).GetComponentInChildren<Text>();
        }
        temporizador = GameObject.Find("Temporizador").GetComponent<Temporizador>();
    }

    /**
    *   Se muestran los Txt de la respuesta del usuario y el problema a resolver
    *   Se llama al método Ajustar()
    */
	public void Comenzar()
	{
        for (int i = 0; i < binarios.Length; i++)
            binarios[i].Activar();
        Ajustar ();
	}

    /**
    *   Se reinicia el Txt de la respuesta del jugador y se genera otro número a calcular
    *   Se apagan todos los números
    */
	public void Ajustar()
	{
        respuestaTxt.text = "0";
		decimalTxt.text = "" + Random.Range (1, 256);
        for (int i = 0; i < binarios.Length; i++)
        {
            binariosTxt[i].text = "0";
            binarios[i].CambiarColor(true);
        }
	}

    /**
    *   Apaga o enciende el botón que presionó el jugador
    *   @param posicion Es la posición (0-7) del botón que presionó el jugador
    */
	public void Cambio(int posicion)
	{
        binarios[posicion].CambiarColor(false);
        if (binariosTxt[posicion].text == "0") 
		{
			binariosTxt[posicion].text = "1";
			Binario (posicion, true);
		} 
		else 
		{
			binariosTxt[posicion].text = "0";
			Binario (posicion, false);
		}
    }

    /**
    *   Dependiendo del botón que presionó el jugador se suma el valor del botón a la respuesta del jugador
    *   Si la respuesta del jugador es igual al problema se llama al método Ajustar()
    *   @param posicion Es la posición (0-7) del botón que presionó el jugador
    *   @param Estado Representa si el botón está encendido o apagado
    */
    public void Binario(int posicion, bool estado)
	{
		byte res = byte.Parse(respuestaTxt.text);
		switch (estado) 
		{
			case true:
			res += (byte) (Mathf.Pow (2, posicion));
				break;
			case false:
			res -= (byte) (Mathf.Pow (2, posicion));
			break;
		}
		respuestaTxt.text = "" + res;
        if (decimalTxt.text == respuestaTxt.text)
        {
            puntuacion += int.Parse(respuestaTxt.text) * 10;
            StopCoroutine("SumaPuntuacion");
            StartCoroutine("SumaPuntuacion", puntuacion);
            temporizador.ReestablecerReloj();
            Ajustar();
        }
	}

    /**
    *   Corrutina que se repite cada frame, suma en el puntuacionTxt la cantidad de puntos acumulada
    *   por el jugador.
    */
    public IEnumerator SumaPuntuacion(int numero)
    {

        while (int.Parse(puntuacionTxt.text) < numero)
        {
            yield return 0;
            puntuacionTxt.text = "" + (int.Parse(puntuacionTxt.text) + 5);
        }
    }
}