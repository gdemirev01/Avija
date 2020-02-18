using UnityEngine;

public class MainMenuUI : Singleton<MainMenuUI>
{
    [SerializeField]
    private GameObject continueButton;

    void Start()
    {
        if(BinarySerializer.LoadPlayerProgress() == null)
        {
            continueButton.SetActive(false);
        }       
    }
}
