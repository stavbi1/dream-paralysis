using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject startTextGO;
    public GameObject exitTextGO;

    private Text startText;
    private Text exitText;
    private Camera fpsCamera;
    private float range = 50f;

    void Start()
    {
        fpsCamera = GetComponent<Camera>();
        startText = startTextGO.GetComponent<Text>();
        exitText = exitTextGO.GetComponent<Text>();
    }

    void Update()
    {
        onHoverExit(startText);
        onHoverExit(exitText);

        CheckInput();
    }

    private void CheckInput()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("start"))
            {
                onHover(startText);
                
                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene(2);
                }
            }
            else if (hit.transform.CompareTag("exit"))
            {
                onHover(exitText);

                if (Input.anyKeyDown)
                {
                    Application.Quit();
                }
            }
        }
    }

    private void onHover(Text text)
    {
        text.color = new Color(0.4198f, 0.5627f, 1, 1);
    }

    private void onHoverExit(Text text)
    {
        text.color = new Color(1, 1, 1, 1);
    }
}
