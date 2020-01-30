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

    public List<GameObject> openedPanels;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        openedPanels = new List<GameObject>();
    }

    public void TogglePanel(GameObject panel, bool state)
    {
        panel.GetComponent<CanvasGroup>().alpha = state ? 1 : 0;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = state;
        panel.GetComponent<CanvasGroup>().interactable = state;

        if (state) { openedPanels.Add(panel); }
        else { openedPanels.Remove(panel); }
    }

    public GameObject GetLastPanel()
    {
        if(openedPanels.Count == 0) { return null; }
        return openedPanels[openedPanels.Count - 1];
    }

    public void LoadText(TextMeshProUGUI container, string text)
    {
        container.text = text;
    }
}
