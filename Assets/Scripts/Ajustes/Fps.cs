//Código creado por Aarón Angulo

using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    private int fps;
    private Text fps_Text;
    private Slider slider;

	// Use this for initialization
	void Start ()
    {
        fps = PlayerPrefs.GetInt("fps");
        fps_Text = GetComponentInChildren<Text>();
        fps_Text.text = fps + "Fps";
        slider = GetComponent<Slider>();

        if (fps == 60)
            slider.value = 1;
        else
            slider.value = 0;
    }

    public void CambiarFps()
    {
        if (slider.value == 1)
            fps = 60;
        else
            fps = 30;

        fps_Text.text = fps + "Fps";
        Application.targetFrameRate = fps;
        PlayerPrefs.SetInt("fps", fps);
    }
}
