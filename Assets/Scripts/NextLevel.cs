using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public string nextLevelName;

    public void LoadNextLevelScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        Time.timeScale = 1;
    }
}
