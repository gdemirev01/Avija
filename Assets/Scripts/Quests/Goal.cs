﻿using System.Collections;
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

    public Goal choice = null;

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

    public void ChooseOption(int index)
    {
        this.choice = options[index];
    }

    public bool MustChoose()
    {
        return done && choice == null && options.Count > 0;
    }

    public bool ReachedEndOfGoal()
    {
        return this.options.Count == 0 && done;
    }

    public enum GoalType
    {
        Kill,
        Talk,
        Gather
    }

    public override string ToString()
    {
        return
               this.type + " " + this.target + this.progress + "/" + this.quantity;
    }
}
