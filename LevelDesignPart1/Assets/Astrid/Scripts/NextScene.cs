using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField] CanvasGroup _Image;
    [SerializeField] int SceneNumber;

    private void Awake()
    {
        if (_Image == null)
        {
            enabled = false;
        }

        StartCoroutine(FadeAnimation(true));
    }

    public void OpenNextScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void StartSceneTransition()
    {
        StartCoroutine(FadeAnimation(false));
    }

    private IEnumerator FadeAnimation(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            
            _Image.alpha = 1;
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set alpha as i
                _Image.alpha = i;
                yield return null;
            }
            _Image.alpha = 0f;
        }
        // fade from transparent to opaque
        else
        {
            _Image.alpha = 0f;
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set alpha as i
                _Image.alpha = i;
                yield return null;
            }

            _Image.alpha = 1;

            // Change scene
            SceneManager.LoadScene(SceneNumber);
        }
    }
}
