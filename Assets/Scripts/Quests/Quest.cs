using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string name;
    public string giver;
    public string text;
    public int partsNumber;
    public int currentPart;
    public float exp;
    public int coins;
    public List<SortedDictionary<string, string>> parts;
    

    public Quest(string name, string giver, string text, int partsNumber, float exp, int coins, List<SortedDictionary<string, string>> parts)
    {
        this.name = name;
        this.giver = giver;
        this.text = text;
        this.partsNumber = partsNumber;
        this.currentPart = 1;
        this.exp = exp;
        this.coins = coins;
        this.parts = parts;
    }

    public SortedDictionary<string, string> GetCurrentPart()
    {
        return this.parts[this.currentPart - 1];
    }

    public string GetCurrentPartType()
    {
        return GetCurrentPart()["type"];
    }

    public string GetCurrentPartStatus()
    {
        return GetCurrentPart()["status"];
    }

    public override bool Equals(object obj)
    {
        var quest = obj as Quest;
        return quest != null &&
               name == quest.name &&
               giver == quest.giver &&
               text == quest.text &&
               partsNumber == quest.partsNumber &&
               currentPart == quest.currentPart &&
               EqualityComparer<List<SortedDictionary<string, string>>>.Default.Equals(parts, quest.parts);
    }

    public override int GetHashCode()
    {
        var hashCode = 1952372838;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(giver);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(text);
        hashCode = hashCode * -1521134295 + partsNumber.GetHashCode();
        hashCode = hashCode * -1521134295 + currentPart.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<List<SortedDictionary<string, string>>>.Default.GetHashCode(parts);
        return hashCode;
    }

    public override string ToString()
    {
        return "Name: " + this.name + " Giver: " + this.giver + " Text" + this.text;
    }
}
