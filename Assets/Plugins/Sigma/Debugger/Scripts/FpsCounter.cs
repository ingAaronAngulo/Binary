using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{ 
    private Text TxtFps;
    private float deltaTime = 0.0f;
    private int fps;
    private float msec;

    void Start()
    {
        TxtFps = GetComponent<Text>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        fps = (int) (1 / deltaTime);
        msec = deltaTime * 1000.0f;
        TxtFps.text = string.Format("{0:0.0} ms ({1:0.}fps)", msec, fps); ;
    }
}
