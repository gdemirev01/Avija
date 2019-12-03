﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string name;
    public string giver;
    public string text;
    public List<SortedDictionary<string, string>> parts;
    

    public Quest(string name, string giver, string text, List<SortedDictionary<string, string>> parts)
    {
        this.name = name;
        this.giver = giver;
        this.text = text;
        this.parts = parts;
    }

    public override bool Equals(object obj)
    {
        var quest = obj as Quest;
        return quest != null &&
               name == quest.name &&
               giver == quest.giver &&
               text == quest.text &&
               EqualityComparer<List<SortedDictionary<string, string>>>.Default.Equals(parts, quest.parts);
    }

    public override int GetHashCode()
    {
        var hashCode = 1892797971;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(giver);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(text);
        hashCode = hashCode * -1521134295 + EqualityComparer<List<SortedDictionary<string, string>>>.Default.GetHashCode(parts);
        return hashCode;
    }

    public override string ToString()
    {
        return "Name: " + this.name + " Giver: " + this.giver + " Text" + this.text;
    }
}
