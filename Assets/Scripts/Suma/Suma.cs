//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage
 * Codigo que maneja la lógica en el modo de juego Sumatoria
*/
public class Suma : MonoBehaviour 
{
    //puntuacion es igual a el número decimal x10
    private GameObject[] numeros = new GameObject[8];
    private NumeroSuma[] numerosScript = new NumeroSuma[8];
    private Temporizador temporizador;
    private UnityEngine.UI.Text sumatoriatxt;
    private UnityEngine.UI.Text respuestatxt;
    private UnityEngine.UI.Text puntuacionTxt;
    private short sumatoria;
	private short respuesta;
	private int puntuacion;
    
    /**
    *  Se comienza encontrando los gameObjects e inicializando variables.
    */
	void Start () 
	{
		sumatoriatxt = GameObject.Find ("sumatoriatxt").GetComponent<UnityEngine.UI.Text>();
		respuestatxt = GameObject.Find ("respuestatxt").GetComponent<UnityEngine.UI.Text>();
		puntuacionTxt = GameObject.Find ("Puntuacion").GetComponent<UnityEngine.UI.Text>();
		sumatoria = 0;
		for (int i = 0; i < numeros.Length; i++)
		{
			numeros [i] = GameObject.Find ("" + Mathf.Pow (2, i));
			numerosScript[i] = numeros[i].GetComponent<NumeroSuma>();
		}
        temporizador = GameObject.Find("Temporizador").GetComponent<Temporizador>();
    }

    /**
    *   Se cambia la propiedad interactable de todos los números que son botones
    *   Se llama al método ajustar.
    */
	public void Comenzar()
	{
        for (int i = 0; i < numerosScript.Length; i++)
            numerosScript[i].Activar();
		Ajustar ();
	}

    /**
    *   Se crea un número valido para ser resuelto y se le asigna al Txt pertinente
    */
	public void Ajustar()
	{
		respuesta = (short) Random.Range (1, 256);
		sumatoria = 0;
		sumatoriatxt.text = "" + sumatoria;
		respuestatxt.text = "" + respuesta;
		for (int i = 0; i < numeros.Length; i++)
			numerosScript[i].preApa(true);
	}

    /**
    *   Se suma el valor de la respuesta del usuario y verifica si ha llegado al resultado
    *   de ser así llama al método Ajustar() y a la corrutina SumaPuntuacion()
    */
	public void Sumar(NumeroSuma suma)
	{
		if (suma.getEncendido())
			sumatoria += suma.getNumero();
		else
			sumatoria -= suma.getNumero();

		sumatoriatxt.text = "" + sumatoria;

		if (sumatoria == respuesta) 
		{
			puntuacion += respuesta * 10;
			StopCoroutine("SumaPuntuacion");
			StartCoroutine("SumaPuntuacion", puntuacion);
            temporizador.ReestablecerReloj();
            Ajustar ();
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
