using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : Singleton<UIController>
{
    private List<GameObject> openedPanels;

    [SerializeField]
    private TextMeshProUGUI alert;

    public override void Awake()
    {
        base.Awake();
        openedPanels = new List<GameObject>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        alert.faceColor = new Color32(255, 0, 0, 255);
    }

    public void TogglePanel(GameObject panel, bool state)
    {
        var canvasGroup = panel.GetComponent<CanvasGroup>();

        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.blocksRaycasts = state;
        canvasGroup.interactable = state;

        if (state)
        {
            openedPanels.Add(panel); 
        }
        else
        { 
            openedPanels.Remove(panel);
        }
    }

    public int GetOpenedPanelsSize()
    {
        return openedPanels.Count;
    }

    public void ClosePanel(GameObject panel)
    {
        TogglePanel(panel, false);
    }

    public void TogglePanelAuto(GameObject panel)
    {
        if (IsPanelOpen(panel))
        {
            TogglePanel(panel, false);
        }
        else
        {
            TogglePanel(panel, true);
        }
    }

    public GameObject GetLastPanel()
    {
        if(openedPanels.Count == 0)
        {
            return null;
        }
        return openedPanels[openedPanels.Count - 1];
    }

    public void SetLastPanel(GameObject panel)
    {
        if(openedPanels.Contains(panel)) 
        {
            openedPanels.Remove(panel);
        }
        openedPanels.Add(panel);
    }

    public bool IsPanelOpen(GameObject panel)
    {
        return openedPanels.Contains(panel);
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

    public void ClearChildrenOfPanel(GameObject panel)
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public T[] CreateSlots<T>(GameObject panel, GameObject slotPrefab, int numberOfSlots)
    {
        T[] slots = new T[numberOfSlots];

        ClearChildrenOfPanel(panel);

        for (int i = 0; i < numberOfSlots; i++)
        {
            var slot = Instantiate(slotPrefab, panel.transform, false);
            slots[i] = slot.GetComponent<T>();
        }

        return slots;
    }

    public void LoadTextToSlots(TextSlot[] slots, string[] info)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].text.text = info[i];
        }
    }

    public void LoadItemsToSlots(ItemSlot[] slots, ItemAmount[] items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].AddItem(items[i]);
        }
    }
}
