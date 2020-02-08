using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    #region Singleton
    public static UIController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of uiController");
            return;
        }

        instance = this;
    }
    #endregion

    private List<GameObject> openedPanels;

    [SerializeField]
    private TextMeshProUGUI alert;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        openedPanels = new List<GameObject>();

        alert.faceColor = new Color32(255, 0, 0, 255);
    }

    public void TogglePanel(GameObject panel, bool state)
    {
        var canvasGroup = panel.GetComponent<CanvasGroup>();

        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.blocksRaycasts = state;
        canvasGroup.interactable = state;

        if (state) { openedPanels.Add(panel); }
        else { openedPanels.Remove(panel); }
    }

    public GameObject GetLastPanel()
    {
        if(openedPanels.Count == 0) { return null; }
        return openedPanels[openedPanels.Count - 1];
    }

    public bool isPanelOpened(GameObject panel)
    {
        return panel.GetComponent<CanvasGroup>().alpha == 1;
    }

    public void LoadText(TextMeshProUGUI container, string text)
    {
        container.text = text;
    }

    public void SetAlertMessage(string message)
    {
        this.alert.text = message;

        Invoke("ClearAlert", 2f);
    }

    private void ClearAlert()
    {
        this.alert.text = "";
    }
}
