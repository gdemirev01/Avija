using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public List<GameObject> openedPanels;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TogglePanel(GameObject panel, bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        panel.GetComponent<CanvasGroup>().alpha = state ? 1 : 0;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = state;
        panel.GetComponent<CanvasGroup>().interactable = state;

        if (state) { openedPanels.Add(panel); }
        else { openedPanels.RemoveAt(openedPanels.IndexOf(panel)); }
    }

    public GameObject GetLastPanel()
    {
        return openedPanels[openedPanels.Count - 1];
    }

    public void LoadText(TextMeshProUGUI container, string text)
    {
        container.text = text;
    }
}
