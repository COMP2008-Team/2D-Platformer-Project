using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Time.timeScale = 1; 
       ;
    }
}
