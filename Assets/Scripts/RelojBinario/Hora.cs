//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

/**
 * @author Angulo Armenta Aarón Orlando (Penchi)
 * @mainpage
 * Se activan y/o desactivan las luces de los nodos que representan el reloj binario
 */
public class Hora : MonoBehaviour 
{
    private UnityEngine.UI.Text puntuacionTxt;
    private UnityEngine.UI.Text respuestaTxt;
    private Focos[] focos = new Focos[13];
    private Temporizador temporizador;
    private string respuesta;
    private string pregunta;
    private int puntuacion;
    private short suma = 0;

    public Color gris;
	/**
	 * Al momento de iniciar el objeto, se mueve la posición de éste, se encuentran e inicializan los
	 * nodos, se inicializa el campo de respuesta a "inexistente" (dando a entender que el usuario no ha dado
	 * ninguna.
	*/
	void Start () 
	{
        focos[0] = GameObject.Find("Hora_01").GetComponent<Focos>();
        focos[1] = GameObject.Find("Hora_02").GetComponent<Focos>();
        focos[2] = GameObject.Find("Hora_04").GetComponent<Focos>();
        focos[3] = GameObject.Find("Hora_08").GetComponent<Focos>();
        focos[4] = GameObject.Find("Hora_10").GetComponent<Focos>();
        focos[5] = GameObject.Find("Hora_20").GetComponent<Focos>();

        focos[6] = GameObject.Find("Minuto_01").GetComponent<Focos>();
        focos[7] = GameObject.Find("Minuto_02").GetComponent<Focos>();
        focos[8] = GameObject.Find("Minuto_04").GetComponent<Focos>();
        focos[9] = GameObject.Find("Minuto_08").GetComponent<Focos>();
        focos[10] = GameObject.Find("Minuto_10").GetComponent<Focos>();
        focos[11] = GameObject.Find("Minuto_20").GetComponent<Focos>();
        focos[12] = GameObject.Find("Minuto_40").GetComponent<Focos>();

        puntuacionTxt = GameObject.Find ("Puntuacion").GetComponent<UnityEngine.UI.Text>();
        respuestaTxt = GameObject.Find("Respuesta").GetComponent<UnityEngine.UI.Text>();

        temporizador = GameObject.Find("Temporizador").GetComponent<Temporizador>();
    }

    /**
	 * Se ejecuta cuando el jugador acierta la hora. 
	 * Establece la interfaz para un nuevo problema a resolver y aumenta la puntuación.
	 */
    public void Ajustar()
	{
		suma = 0;
		pregunta = "";
		respuesta = "";
		respuestaTxt.text = respuesta;
        for (int i = 0; i < focos.Length; i++)
            focos[i].Apagar();
        //Se repite por el número de focos que representan la hora (6)
        for (int i = 0; i < 13; i++)
        {
            if (Random.Range(0, 2) == 1)
            {
                if (i < 7)
                {
                    //Estamos en horas
                    if (i == 3)
                        if ((focos[1].GetEncendido()) || (focos[2].GetEncendido()))
                            break;
                    if (i == 5)
                        if ((focos[2].GetEncendido()) || (focos[3].GetEncendido()) || (focos[4].GetEncendido()))
                            break;
                }
                else
                {
                    //Estamos en minutos
                    if (i == 9)
                        if ((focos[8].GetEncendido()) || (focos[7].GetEncendido()))
                            break;
                    if (i == 12)
                        if (focos[11].GetEncendido())
                            break;
                }
                suma += focos[i].GetValor();
                focos[i].Prender();
            }
        }
		pregunta = "" + suma;
		switch (pregunta.Length) 
		{
			case 0:
				pregunta = "0000";
				break;
			case 1:
				pregunta = "000" + pregunta;
			break;

			case 2:
			pregunta = "00" + pregunta;
			break;

			case 3:
			pregunta = "0" + pregunta;
			break;
		}
		pregunta = pregunta.Substring (0, 2) + ":" + pregunta.Substring (2, 2);
    }

    /**
	 * Dibuja el número que será la respuesta del jugador en la interfaz
	 * @param numero Es el número que ha seleccionado el jugador.
	 */
    public void marcar(int numero)
	{
		//Si presiona "Atrás".
		if (numero == 10)
		{
			if (respuesta.Length > 0)
			{
				if(respuesta.Length == 4)
					respuesta = respuesta.Substring (0, respuesta.Length - 2);
				else
					respuesta = respuesta.Substring (0, respuesta.Length - 1);
				respuestaTxt.color = gris;
			}
		}

		//Si presiona cualquier número.
		else 
		{
			if(respuesta.Length == 2)
				respuesta = respuesta.Substring(0, 2) + ":";
			
			if(respuesta.Length != 5)
				respuesta = respuesta + numero;
		}
		respuestaTxt.text = respuesta;
	}

	/**
	 * Verifica (duh) si la respuesta del jugador es correcta, de serlo se inicializa la corrutina Sumar()
	 * llama al método Ajustar() inicianzo otro problema.
	 */
	public void Verificar()
	{
		if (respuesta != pregunta)
			respuestaTxt.color = Color.red;
		else
		{
			respuestaTxt.color = gris;
			puntuacion = (puntuacion + ((int)(pregunta[0]) + (int) pregunta[1] + (int) pregunta[3] + (int)pregunta[4] - 188) * 100);
			PlayerPrefs.SetInt("UltimaPuntuacionReloj", puntuacion);
			StopCoroutine("Sumar");
			StartCoroutine("Sumar", puntuacion);
            temporizador.ReestablecerReloj();
			Ajustar();
		}
	}

	/**
	 * Cuando el jugador acierta el problema se suma gradualmente su puntuación dependiendo
	 * del nivel de dificultad del problema.
	 * @param numero La puntuación real del jugador.
	 */
	public IEnumerator Sumar(int numero)
	{
		while (int.Parse(puntuacionTxt.text) < numero) 
		{
            yield return (0);
            puntuacionTxt.text = "" + (int.Parse(puntuacionTxt.text) + 10);
		}
	}

	/**
	 * Se utiliza solo al inicio, para hacer los botones interactuables
	 */
	public void Activar()
	{
		for (int i = 0; i < 10; i++) 
		{
			GameObject.Find ("" + i).GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}
		GameObject.Find ("verificarBtn").GetComponent<UnityEngine.UI.Button> ().interactable = true;
		GameObject.Find ("borrarBtn").GetComponent<UnityEngine.UI.Button> ().interactable = true;
	}
}
