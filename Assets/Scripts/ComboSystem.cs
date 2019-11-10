using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{

    private Animator animator;
    public string streakName;
    private int comboCounter;
    public float reactionTime;

    private float timeLeft;
    private bool timerRunning = false;

    void Start()
    {
        comboCounter = 0;
        timeLeft = reactionTime;
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger(streakName, comboCounter);

        if (timerRunning)
        {
            UpdateTimer();
        }
    }

    public void IncreaseStreak()
    {
        comboCounter++;

        if(comboCounter > 3)
        {
            comboCounter = 3;
        }
    }

    public void ResetStreak()
    {
        comboCounter = 0;
    }

    private void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            ResetStreak();
            timerRunning = false;
            timeLeft = reactionTime;
        }
    }

    public void RestartTimer()
    {
        timeLeft = reactionTime;
        timerRunning = true;
    }
}
