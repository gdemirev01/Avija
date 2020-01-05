using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goal", menuName = "Quests/Goal")]
public class Goal : ScriptableObject
{
    public GoalType type;
    public int quantity;
    public int progress;
    //public string Zone;
    public string target;
    public bool done;

    [SerializeField]
    public List<Goal> options;
    public List<Goal> Options
    {
        get
        {
            if (options == null)

            {
                options = new List<Goal>();
            }

            return options;
        }
    }

    public static T Create<T>(string name)
    where T : Goal
    {
        T node = CreateInstance<T>();
        node.name = name;
        return node;
    }

    public Goal GetCurrentOption()
    {
        Goal currentGoal = null;

        foreach(Goal option in options)
        {
            if(option.done)
            {
                currentGoal = option.GetCurrentOption();
            }
        }

        if(currentGoal == null)
        {
            return this;
        }
        else
        {
            return currentGoal;
        }
    }

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
