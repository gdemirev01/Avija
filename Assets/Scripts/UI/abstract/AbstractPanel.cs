using UnityEngine;

public abstract class AbstractPanel : MonoBehaviour, IPanel
{
    private UIController uIController;

    [SerializeField]
    private GameObject panel;

    private void Start()
    {
        this.uIController = UIController.instance;
    }

    public void TogglePanel(bool state)
    {
        uIController.TogglePanel(this.panel, state);
    }

    public abstract void ClearPanel();
}
