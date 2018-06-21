//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;

/**
 * @author Penchi 
 * @mainpage
 * Guarda y carga las opciones seleccionadas por el jugador tales como música, colores, nombre Online, etc.
 */
public class Colores : MonoBehaviour 
{
	private ColorBlock colores;
    /**
    *   Constructor que inicializa los valores de las variables a las opciones guardadas por el jugador
    */
    public ColorBlock getColorNumeros()
    {
        colores.normalColor = new Color(32.0f / 255, 149.0f / 255, 242.0f / 255);

        colores.highlightedColor = colores.normalColor;
        colores.pressedColor = colores.normalColor;
        colores.disabledColor = new Color(113.0f / 255, 113.0f / 255, 113.0f / 255);

        colores.colorMultiplier = 1;
        colores.fadeDuration = 0.125F;
        return colores;
    }

    public ColorBlock getColorBinarios()
    {
        colores.normalColor = new Color(243.0f / 255, 66.0f / 255, 53.0f / 255);

        colores.highlightedColor = colores.normalColor;
        colores.pressedColor = colores.normalColor;
        colores.disabledColor = new Color(113.0f / 255, 113.0f / 255, 113.0f / 255);

        colores.colorMultiplier = 1;
        colores.fadeDuration = 0.125F;
        return colores;
    }

}
