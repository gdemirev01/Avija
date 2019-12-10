using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goal
{
    public GoalType type;
    public int quantity;
    public int progress;
    //public string Zone;
    public string target;
    public bool done;

    public enum GoalType
    {
        Kill,
        Talk,
        Gather
    }

    public override string ToString()
    {
        return this.type + " " + this.target + this.progress + "/" + this.quantity;
    }
}
