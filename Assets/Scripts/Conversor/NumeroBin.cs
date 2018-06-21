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
public class NumeroBin : MonoBehaviour
{
    /*
		colorBtn es ÉSTE botón
		color son los colores del usuario
		colorRes es el respaldo del color de las opciones
	*/
    public Color gris;
    private Button colorBtn;
    private ColorBlock colorRes;
    private ColorBlock color;

    /**
    *   Se comienza con los numeros apagados, se asigna el valor del número tomando en cuenta su nombre,
    *   se cargan los colores de la clase opciones en la camara.
    *   Se llama al método CambiarColor()
    */
    void Start ()
    {
        colorBtn = GetComponent<Button>();
        color = new Colores().getColorBinarios();
        colorRes = color;
        colorBtn.colors = color;
        CambiarColor(false);
    }

    /**
    *   Activa el botón para hacerlo interactuable
    */
    public void Activar()
    {
        colorBtn.interactable = true;
    }


    public void CambiarColor(bool reiniciar)
    {
        
        if ((colorBtn.colors.normalColor == colorRes.normalColor) || (reiniciar))
        {
            color.normalColor = gris;
            color.highlightedColor = gris;
        }
        else
        {
            color.normalColor = colorRes.normalColor;
            color.highlightedColor = colorRes.highlightedColor;
        }
        colorBtn.colors = color;
    }
}
