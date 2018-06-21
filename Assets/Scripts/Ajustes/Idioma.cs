//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Idioma : MonoBehaviour
{
    private Dropdown dd;

	void Start ()
    {
        dd = GetComponent<Dropdown>();
        dd.value = PlayerPrefs.GetInt("index");
    }
	
    public void CambiarIdioma()
    {
        switch (dd.value)
        {
            case 0:
                PlayerPrefs.SetString("idioma", "Spanish");
                break;
            case 1:
                PlayerPrefs.SetString("idioma", "English");
                break;
        }
        PlayerPrefs.SetInt("index", dd.value);
        new Dialogos().Ajustes();
    }
}
