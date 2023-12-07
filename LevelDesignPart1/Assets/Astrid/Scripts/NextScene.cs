using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void OpenNextScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
