using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuVideo : MonoBehaviour
{

    private VideoPlayer video;
    void Start()
    {
        video = GetComponent<VideoPlayer>();
        StartCoroutine("WaitForMovieEnd");
    }


    public IEnumerator WaitForMovieEnd()
    {
        Debug.Log(!video.isPrepared + " " + video.isPlaying);
        while (!video.isPrepared || video.isPlaying)
        {
            yield return new WaitForSeconds(0.3f);

        }
        OnMovieEnded();
    }

    void OnMovieEnded()
    {
        SceneManager.LoadScene(1);
    }
}
