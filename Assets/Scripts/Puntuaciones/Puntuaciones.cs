//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage 
 * Administra las puntuaciones totales, de la partida recien jugada y la mejor puntuación
 * hecha por el jugador
 */
public class Puntuaciones : MonoBehaviour 
{
    private AnimPuntuacion animaciones;
    private Text puntuacionPartidaTxt;
	private Text mejorPuntuacionTxt;
    private SQL baseDeDatos = new SQL();

    /**
	 * Se empieza inicializando los objetos con referencias pertinentes, llama a la corrutina Voltear(),
	 * llama al método Cargar() y Desplegar().
	 */
    void Start () 
	{
        new Dialogos().Puntuaciones();
        animaciones = GetComponent<AnimPuntuacion>();
        animaciones.Iniciar();
        baseDeDatos.Iniciar();
        puntuacionPartidaTxt = GameObject.Find ("EstaPuntos_Txt").GetComponent<Text>();
		mejorPuntuacionTxt = GameObject.Find ("MejorPuntos_Txt").GetComponent<Text>();
        CargarPuntuaciones();
        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
            Screen.orientation = ScreenOrientation.Portrait;

    }

    /**
	 * Sobreescribe (de ser necesario) la mejor puntuación hecha por el jugador y toma la puntuación realizada en la ultima partida
	 */
    public void CargarPuntuaciones()
	{
        int mejorPuntuacion = baseDeDatos.MayorPuntuacion(Global.modoJuego, Global.varianteJuego);
        int puntuacionActual = Global.puntuacionParcial;
        if (puntuacionActual > mejorPuntuacion)
        {
            puntuacionPartidaTxt.text += puntuacionActual;
            mejorPuntuacionTxt.text += puntuacionActual;
            baseDeDatos.Actualizar(Global.modoJuego, Global.varianteJuego, puntuacionActual);
        }
        else
        {
            puntuacionPartidaTxt.text += puntuacionActual;
            mejorPuntuacionTxt.text += mejorPuntuacion;
        }
    }

    /**
     * Vuelve al menú de inicio
     */
	public void Volver()
	{
        animaciones.Destruir(0);
	}

    public void Reiniciar()
    {
        animaciones.Destruir(1);
    }
}
