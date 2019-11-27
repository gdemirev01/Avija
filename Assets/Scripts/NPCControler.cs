using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public class NPCControler : MonoBehaviour
{
    public float range = 5;
    public bool playerInRange = false;
    private GameObject Player;
    private UIController canvasController;
    private GameObject missionPanel;
    public string questName;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        canvasController = GameObject.Find("Canvas").GetComponent<UIController>();
        missionPanel = GameObject.Find("MissionPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {


        string path = "Assets/Resources/Quests/" + questName + ".json";
        Quest quest = QuestParser.Deserialize(path);

        canvasController.TogglePanel(missionPanel, true);
        canvasController.LoadQuest(quest);
    }
}
