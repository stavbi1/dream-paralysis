using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    private Camera fpsCamera;
    private float range = 50f;

    void Start()
    {
        fpsCamera = GetComponent<Camera>();
    }

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Start"))
            {
                hit.transform.GetComponent<>
                if (Input.anyKeyDown)
                {
                    SceneManager.LoadScene(2);
                }
            }
            else if (hit.transform.CompareTag("Exit"))
            {
                if (Input.anyKeyDown)
                {
                    Application.Quit();
                }
            }
        }
    }
}
