using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{

    public Animator animator;
    public float reactionTime;

    private float timeLeft;
    private bool timerRunning = false;

    void Start()
    {
        timeLeft = reactionTime;
    }

    void Update()
    {
        if (timerRunning)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            animator.SetBool("attacking", false);
            timerRunning = false;
            timeLeft = reactionTime;
        }
    }

    public void RestartTimer()
    {
        animator.SetBool("attacking", true);
        timeLeft = reactionTime;
        timerRunning = true;
    }
}
