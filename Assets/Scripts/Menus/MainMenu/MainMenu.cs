using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Continue()
    {
        PlayerData playerData = BinarySerializer.LoadPlayerProgress();
        SaveSystem.playerData = playerData;

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
