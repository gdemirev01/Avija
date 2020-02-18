using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Respawn()
    {
        PlayerData playerData = BinarySerializer.LoadPlayerProgress();
        SaveSystem.playerData = playerData;

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
