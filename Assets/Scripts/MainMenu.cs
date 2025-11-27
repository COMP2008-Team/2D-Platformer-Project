using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
