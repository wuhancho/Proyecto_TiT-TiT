using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoSceneChanger : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] public Image fadeImage; 
    public float fadeDuration = 1.5f; 

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        if (fadeImage != null)
        {
            Color tempColor = fadeImage.color;
            tempColor.a = 0f;
            fadeImage.color = tempColor;
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(FadeAndChangeScene());
    }

    private IEnumerator FadeAndChangeScene()
    {
        float t = 0f;
        Color tempColor = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            tempColor.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = tempColor;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeSceneEspecifivic(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}