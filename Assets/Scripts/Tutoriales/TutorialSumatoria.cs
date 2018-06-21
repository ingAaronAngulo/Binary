//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialSumatoria : MonoBehaviour
{
    private string[] textos = new string[2];

    void Awake()
    {
        textos = new Dialogos().TutorialSumatoria();

        GameObject.Find("TxtSumatoria").GetComponent<Text>().text = textos[0];
        GameObject.Find("Cerrar").GetComponent<Text>().text = textos[1];
    }

    public void Cerrar()
    {
        Destroy(gameObject);
    }
}
