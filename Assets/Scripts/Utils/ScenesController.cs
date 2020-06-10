using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : Singleton<ScenesController>
{
    public void ExitMain(string nextScene)
    {
        SaveSystem.Instance.Save();
        SaveSystem.playerData = null;

        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}
