//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;
/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage
 * Se asignan los colores a los nodos del reloj binario
 */
public class Focos : MonoBehaviour 
{
    private Image color;
    private bool encendido;
    private short valor;

    public Color prendido;
    public Color apagado;
	/**
	 * Se comienza cargando las preferencias de color para los nodos del reloj binario 
	 */
	void Start () 
	{
        color = GetComponent<Image>();
        encendido = false;
        valor = 0;

        //Memazo valor = short.Parse(nombre.Substring(nombre.Length - 2)) * 100;
        string nombre = GetComponentInParent<Transform>().name;
        if (nombre.Substring(0, 1) == "H")
        {
            valor = short.Parse(nombre.Substring(nombre.Length - 2));
            valor *= 100;
        }
        else
            valor = short.Parse(nombre.Substring(nombre.Length - 2));

        //prendido = new Color(231.0f / 255, 20.0f / 255, 222.0f / 255);
        //apagado = new Color(222.0f / 255, 213.0f / 255, 192.0f / 255);
    }

    /**
	 * Cambia el color del nodo en cuestión al que haya escogido el jugador
	 * para representar el encendido
	 */
    public void Prender()
    {
        encendido = true;
        color.color = prendido;
    }

    /**
	 * Cambia el color  del nodo en cuestión al que haya escogido el jugador
	 * para representar el apagado
	 */
    public void Apagar()
	{
        encendido = false;
        color.color = apagado;
	}

    /**
     *  @return Devuelve true si el foco está prendido. False si está apagado 
     */
    public bool GetEncendido()
    {
        return encendido;
    }

    /**
     *   @return Devuelve el valor del nodo
     */
    public short GetValor()
    {
        return valor;
    }


}
