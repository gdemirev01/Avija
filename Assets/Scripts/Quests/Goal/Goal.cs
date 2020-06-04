using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goal", menuName = "Quests/Goal")]
public class Goal : ScriptableObject
{
    public string text;
    public GoalType type;
    public int quantity;
    public int progress;
    public string target;

    public bool done;

    public int weight;

    public Goal choice = null;

    public string nextGoalText;

    [SerializeField]
    public List<Goal> options;

    public int CalculateWeight()
    {
        if(this.ReachedEndOfGoal())
        {
            return this.weight;
        }
        return (this.weight + choice.CalculateWeight());
    }

    public Goal GetCurrentChoice()
    {
        if(this.choice == null)
        {
            return this;
        } 
        else
        {
            return choice.GetCurrentChoice();
        }
    }

    public void ChooseOption(Goal option)
    {
        this.choice = option;
    }

    public bool MustChoose()
    {
        return done && choice == null && options.Count > 1;
    }

    public bool ReachedEndOfGoal()
    {
        return this.options.Count == 0 && done;
    }

    public void Reset()
    {
        this.done = false;
        progress = 0;
        choice = null;

        foreach(Goal option in this.options)
        {
            option.Reset();
        }
    }

    public override string ToString()
    {
        return
               this.type + " " + this.target + " " + this.progress + "/" + this.quantity;
    }
}
