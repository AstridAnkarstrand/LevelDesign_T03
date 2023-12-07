using UnityEngine;

public class Skip : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            NextScene nextScene = GetComponent<NextScene>();
            if (nextScene != null)
            {
                nextScene.StartSceneTransition();
            }
        }
    }
}
