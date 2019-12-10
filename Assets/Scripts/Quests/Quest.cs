using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    public string giver;
    public string text;
    public List<Goal> goals;
    public int currentGoal;
    public Reward reward;

    public Goal GetCurrentGoal()
    {
        return this.goals[this.currentGoal];
    }

    public string GetGoalsList()
    {
        string result = "";
        foreach(Goal goal in this.goals)
        {
            result += goal.ToString() + "\n";
        }

        return result;
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
