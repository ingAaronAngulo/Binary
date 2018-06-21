//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/**
* @author Angulo Armenta Aarón Orlando (Penchi)
* @mainpage 
* Controla los eventos desde que el jugador comienza la partida hasta cuando el temporizador
* marca 0 en el reloj, al final despliega la tabla de puntuaciones.
*/
public class Temporizador : MonoBehaviour 
{
	private GameObject comenzar;
	private Text puntuacionesTxt;
	private Text temporizadorTxt;
    private float tiempo;
    private float tiempoRestante;
    private bool detener;
    private string variante;
    private bool fin;

    /**
	 * Se empieza inicializando los valores de los objetos con referencias, iniciar se inicia en falso
	 * para que el tiempo aún no corra.
	 */
    void Start () 
	{
        fin = false;
        temporizadorTxt = GetComponent<Text>();
		comenzar = GameObject.Find ("Comenzar");
        puntuacionesTxt = GameObject.Find("Puntuacion").GetComponent<Text>();

        detener = true;
        variante = Global.varianteJuego;

        comenzar.GetComponentInChildren<Text>().text = new Dialogos().Comenzar();
        switch (variante)
        {
            case "normal":
                tiempoRestante = 60;
                temporizadorTxt.text = "01:00:00";
                break;
            case "survival":
                tiempoRestante = 15;
                temporizadorTxt.text = "00:" + tiempoRestante + ":00";
                break;
            case "contrareloj":
                tiempoRestante = 4;
                temporizadorTxt.text = "00:0" + tiempoRestante + ":00";
                break;
        }
    }

	/**
	 * Hace que el tiempo corra, muestra el tiempo restante en la interfaz y cuando llega a 0 llama al método Parar().
	 */
	void Update () 
	{
		if (!detener) 
		{
            tiempoRestante -= (Time.timeSinceLevelLoad - tiempo);
            tiempo = Time.timeSinceLevelLoad;

			if (tiempoRestante < 10)
				temporizadorTxt.text = "00:0" + Math.Round(tiempoRestante, 2).ToString().Replace(".", ":");
			else
                temporizadorTxt.text = "00:" + Math.Round(tiempoRestante, 2).ToString().Replace(".", ":");
        }

		if ((tiempoRestante <= 0.0) && (!fin))
		{
            fin = true;
            Debugger.Escribir("memes");
			temporizadorTxt.text = "00:00:00";
            Parar();
		}
        
	}

	/**
	 * Se inicia cuando se pulsa el botón para comenzar. 
	 * Desactiva el botón para comenzar la partida, asigna el tiempo que se desea y cambia 
	 * la variable iniciar a true.
	 */
	public void ComenzarWatch()
	{
		Destroy(comenzar);
		tiempo += Time.timeSinceLevelLoad;
        detener = false;
        GameObject.Find("PausaBtn").GetComponent<Button>().interactable = true;
	}

    private float tiempoAuxiliar;
    public void Detener()
    {
        detener = true;
        tiempoAuxiliar = Time.timeSinceLevelLoad;

    }

    public void Resumir()
    {
        tiempoAuxiliar = Time.timeSinceLevelLoad - tiempoAuxiliar;
        tiempoRestante += tiempoAuxiliar;
        detener = false;
    }
    /**
     *  Se llama desde el manager del modo de juego para aumentar o reestablecer el tiempo restante del juego
     *  dependiendo de la variante de juego que seleccionó el jugador
     */
    public void ReestablecerReloj()
    {
        switch (variante)
        {
            case "normal":
                break;
            case "survival":
                tiempoRestante += 2;
                break;
            case "contrareloj":
                tiempoRestante = 4;
                break;
        }
    }
	/**
	 * Establece el valor de la variable "iniciar" a false.
	 * Rota la interfaz visualmente, cuando está en un ángulo no visible se destruye
	 * y crea una instancia de la proxima interfaz a desplegar.
	 */
	public void Parar()
	{
        detener = true;
        Global.puntuacionParcial = int.Parse(puntuacionesTxt.text);
        
        PlayerPrefs.SetInt("publicidad", PlayerPrefs.GetInt("publicidad") + 1);
        
        Global.juego.GetComponent<AnimacionJuegos>().StartCoroutine("Desaparecer");
    }
}
