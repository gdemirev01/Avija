using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string name;
    public string giver;
    public string text;
    public Reward reward;
    public int currentGoal;
    public bool done = false;

    private static int questsNumber = 0;

    [SerializeField]
    private List<Goal> goals;
    public List<Goal> Goals
    {
        get
        {
            if(goals == null)
            {
                goals = new List<Goal>();
            }

            return goals;
        }
    }

    [MenuItem("Quests/Create")]
    public static Quest Create()
    {
        questsNumber++;

        Quest quest = CreateInstance<Quest>();

        string path = string.Format("Assets/Quests/quest{0}.asset", questsNumber);
        AssetDatabase.CreateAsset(quest, path);
        return quest;
    }

    public void AddGoal(Goal goal)
    {
        Goals.Add(goal);
        AssetDatabase.AddObjectToAsset(goal, this);
        AssetDatabase.SaveAssets();
    }

    // modify to show goals after evaluation of player's choices
    public string GetGoalsList()
    {
        string result = "";
        foreach(Goal goal in this.goals)
        {
            result += goal.ToString() + "\n";
        }

        return result;
    }

    public Goal GetCurrentGoal()
    {
        return goals[currentGoal].GetCurrentOption();
    }

    public override bool Equals(object obj)
    {
        var quest = obj as Quest;
        return quest != null &&
               name == quest.name &&
               giver == quest.giver &&
               text == quest.text &&
               EqualityComparer<List<Goal>>.Default.Equals(goals, quest.goals) &&
               currentGoal == quest.currentGoal &&
               EqualityComparer<Reward>.Default.Equals(reward, quest.reward);
    }

    public override int GetHashCode()
    {
        var hashCode = -889736268;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(giver);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(text);
        hashCode = hashCode * -1521134295 + EqualityComparer<List<Goal>>.Default.GetHashCode(goals);
        hashCode = hashCode * -1521134295 + currentGoal.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Reward>.Default.GetHashCode(reward);
        return hashCode;
    }

    public override string ToString()
    {
        return "Name: " + this.name + " Giver: " + this.giver + " Text" + this.text;
    }
}
