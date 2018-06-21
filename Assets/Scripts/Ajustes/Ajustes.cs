//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class Ajustes : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        new Dialogos().Ajustes();
    }

    /**
     * Vuelve al menú de inicio
     */
    public void Volver()
    {
        Global.CrearMenu();
        Destroy(gameObject);
    }

    /**
     * Llama al método para borrar y crear una nueva base de datos
     */
     public void BorrarBdd()
    {
        new SQL().BorrarBdd();
    }

    public void AvisoFps()
    {
        string[] s = new Dialogos().AvisoFps();
        (gameObject.AddComponent(typeof(Advisor)) as Advisor).Crear(s[0], s[1]);
    }
}
