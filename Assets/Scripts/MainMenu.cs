using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
