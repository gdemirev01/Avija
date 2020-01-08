using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    new public string name;
    public string giver;
    public string text;
    public Reward reward;
    public bool done = false;

    private static int questsNumber = 0;

    public Goal goal;

    public override bool Equals(object obj)
    {
        var quest = obj as Quest;
        return quest != null &&
               name == quest.name &&
               giver == quest.giver &&
               text == quest.text &&
               EqualityComparer<Reward>.Default.Equals(reward, quest.reward);
    }

    public override int GetHashCode()
    {
        var hashCode = -889736268;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(giver);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(text);
        hashCode = hashCode * -1521134295 + EqualityComparer<Reward>.Default.GetHashCode(reward);
        return hashCode;
    }

    public override string ToString()
    {
        return "Name: " + this.name + " Giver: " + this.giver + " Text" + this.text;
    }
}
