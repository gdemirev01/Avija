using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private UIController uiController;

    [SerializeField]
    private GameObject inGameMenu;

    private void Start()
    {
        uiController = UIController.Instance;
    }

    public void Quit()
    {
        ScenesController.Instance.ExitMain("Menu");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(uiController.IsPanelOpen(inGameMenu))
            {
                uiController.TogglePanel(inGameMenu, false);
            }
            else if(uiController.GetOpenedPanelsSize() > 0)
            {
                uiController.TogglePanel(uiController.GetLastPanel(), false);
            }
            else
            {
                uiController.TogglePanel(inGameMenu, true);
            }
        }
    }
}
