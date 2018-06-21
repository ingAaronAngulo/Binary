//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;

/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage 
 *  Se cargan colores de las opciones, apaga y enciende los números, devuelve el valor del número y
 *  si está encendido o no.
 * 
 */
public class NumeroSuma : MonoBehaviour 
{
	/*
		colorBtn es ÉSTE botón
		color son los colores del usuario
		colorRes es el respaldo del color de las opciones
	*/
	private bool encendido;
	private short numero;
    public Color gris;
	private Button colorBtn;
	private ColorBlock colorRes;
	private ColorBlock color;
    
    /**
    *   Se comienza con los numeros apagados, se asigna el valor del número tomando en cuenta su nombre,
    *   se cargan los colores de la clase opciones en la camara
    */
    void Start () 
	{
		encendido = false;
		numero = short.Parse(gameObject.name);
		colorBtn = GetComponent<Button>();
		color = new Colores().getColorNumeros();
		colorRes = color;
		colorBtn.colors = color;
	}

    /**
    *   Activa el botón para hacerlo interactuable
    */
    public void Activar()
    {
        colorBtn.interactable = true;
    }
	/**
    *   Método que apaga o enciende los números dependiendo de si están encendidos o apagados
	*	@param reseteo Si es true se apagan todos los numeros
	*/
	public void preApa(bool reiniciar)
	{
		if ((encendido) || (reiniciar))
		{
			encendido = false;
			color.normalColor = gris;
			color.highlightedColor = gris;
		} 
		else 
		{
			encendido = true;
			color.normalColor = colorRes.normalColor;
			color.highlightedColor = colorRes.highlightedColor;
		}

		colorBtn.colors = color;
	}

	/**
	*	@return Devuelve si el número está encendido o apagado
	*/
	public bool getEncendido()
	{
		return encendido;
	}

	/**
	*	@return Devuelve el valor numerico del número (doh)
	*/
	public short getNumero()
	{
		return numero;
	}
}
