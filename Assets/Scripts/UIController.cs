using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject missionPanel;
    public List<GameObject> openedPanels;
    private GameObject description;
    private Button acceptButton;
    private QuestSystem questSystem;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        missionPanel = GameObject.Find("MissionPanel");
        description = missionPanel.transform.GetChild(0).gameObject;
        openedPanels = new List<GameObject>();
        acceptButton = GameObject.Find("AcceptQuestButton").GetComponent<Button>();
        questSystem = transform.GetChild(1).GetComponent<QuestSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadQuest(Quest quest)
    {
        description.GetComponent<TextMeshProUGUI>().text = quest.questText;
        acceptButton.onClick.AddListener(() => questSystem.AddQuest(quest));
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
