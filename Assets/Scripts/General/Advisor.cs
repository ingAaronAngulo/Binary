//Código creado por Aarón Angulo

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Advisor : MonoBehaviour
{
    public GameObject advisor;

    private Vector3 escalaMinima;
    private Vector3 escalaNormal;

    private Image panel;
    private Button btnCerrar;
    private Text txtTitulo;
    private Text txtCuerpo;

    private float velAnimacion;
    private float t;

    void Awake()
    {
        advisor = Instantiate(Resources.Load("Prefabs/Advisor", typeof(GameObject)) as GameObject);
        panel = GameObject.Find("Panel").GetComponent<Image>();
        btnCerrar = GameObject.Find("BtnCerrar").GetComponent<Button>();
        btnCerrar.onClick.AddListener(() => Eliminar());
        txtTitulo = GameObject.Find("TxtAdTitulo").GetComponent<Text>();
        txtCuerpo = GameObject.Find("TxtAdCuerpo").GetComponent<Text>();
        velAnimacion = 3f;
        escalaNormal = panel.transform.localScale;
        escalaMinima = new Vector3();
        panel.transform.localScale = escalaMinima;
    }


    public void Crear(string titulo, string cuerpo)
    {
        txtTitulo.text = titulo;
        txtCuerpo.text = cuerpo;
        StartCoroutine("RepCreacion");
    }

    public void Eliminar()
    {
        StopCoroutine("RepCreacion");
        StartCoroutine("RepEliminacion");
    }

    public IEnumerator RepCreacion()
    {
        float t = 0;
        while (!panel.transform.localScale.Equals(escalaNormal))
        {
            t += velAnimacion * Time.deltaTime;
            panel.transform.localScale = Vector3.Lerp(escalaMinima, escalaNormal, t);
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator RepEliminacion()
    {
        float t = 0;
        while (!panel.transform.localScale.Equals(escalaMinima))
        {
            t += velAnimacion * Time.deltaTime;
            panel.transform.localScale = Vector3.Lerp(escalaNormal, escalaMinima, t);
            yield return new WaitForSeconds(0);
        }
        Destroy(advisor);
    }

    void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}
}
