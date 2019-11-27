using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string name;
    public string questGiver { get; set; }
    public string questText { get; set; }
    public SortedDictionary<string, string> questProps { get; set; }

    public Quest(string name, string questGiver, string questText, SortedDictionary<string, string> questProps)
    {
        this.name = name;
        this.questGiver = questGiver;
        this.questText = questText;
        this.questProps = questProps;
    }

    public override bool Equals(object obj)
    {
        var quest = obj as Quest;
        return quest != null &&
               name == quest.name &&
               questGiver == quest.questGiver &&
               questText == quest.questText &&
               EqualityComparer<SortedDictionary<string, string>>.Default.Equals(questProps, quest.questProps);
    }

    public override int GetHashCode()
    {
        var hashCode = -853884937;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(questGiver);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(questText);
        hashCode = hashCode * -1521134295 + EqualityComparer<SortedDictionary<string, string>>.Default.GetHashCode(questProps);
        return hashCode;
    }
}
