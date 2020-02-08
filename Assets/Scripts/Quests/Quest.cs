using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public int id;
    new public string name;
    public string giver;
    public string text;
    public bool done = false;

    public Goal goal;
    public Reward reward;
    public List<Quest> nextQuests;

    private static int questsCount;

    private void Awake()
    {
        questsCount++;
        this.id = questsCount;
    }

    public Quest GetNextQuest()
    {
        if (!this.done || this.nextQuests.Count < 2) { return null; }

        Quest nextQuest;

        int weight = this.goal.CalculateWeight();

        if (weight <= 0)
        {
            nextQuest = this.nextQuests[0];
        }
        else
        {
            nextQuest = this.nextQuests[1];
        }

        return nextQuest;
    }

    public override bool Equals(object obj)
    {
        return obj is Quest quest &&
               base.Equals(obj) &&
               name == quest.name &&
               giver == quest.giver &&
               text == quest.text &&
               EqualityComparer<Reward>.Default.Equals(reward, quest.reward) &&
               done == quest.done &&
               EqualityComparer<Goal>.Default.Equals(goal, quest.goal) &&
               EqualityComparer<List<Quest>>.Default.Equals(nextQuests, quest.nextQuests);
    }

    public override int GetHashCode()
    {
        var hashCode = -873484624;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(giver);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(text);
        hashCode = hashCode * -1521134295 + EqualityComparer<Reward>.Default.GetHashCode(reward);
        hashCode = hashCode * -1521134295 + done.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Goal>.Default.GetHashCode(goal);
        hashCode = hashCode * -1521134295 + EqualityComparer<List<Quest>>.Default.GetHashCode(nextQuests);
        return hashCode;
    }

    public void Reset()
    {
        this.done = false;
        this.goal.Reset();
    }

    public override string ToString()
    {
        return "Name: " + this.name + " Giver: " + this.giver + " Text" + this.text;
    }
}
