//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;

public class SelecTutorial : MonoBehaviour
{
    private GameObject tutorial;

    void Awake()
    {
        
    }

    public void GenerarTutorial()
    {
        switch (Global.modoJuego)
        {
            case "reloj":
                tutorial = Resources.Load("Prefabs/TutorialReloj", typeof(GameObject)) as GameObject;
                break;

            case "suma":
                tutorial = Resources.Load("Prefabs/TutorialSumatoria", typeof(GameObject)) as GameObject;
                break;

            case "conversor":
                tutorial = Resources.Load("Prefabs/TutorialConversor", typeof(GameObject)) as GameObject;
                break;
        }
        Instantiate(tutorial, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
    }
}
