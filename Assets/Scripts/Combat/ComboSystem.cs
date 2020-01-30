using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private Animator animator;
    private Timer timer;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        timer = GetComponent<Timer>();

        timer.onTimerRestart += OnTimerRestart;
        timer.onTimerEnd += OnTimerEnd;
    }

    public void OnTimerRestart()
    {
        animator.SetBool("attacking", true);
    }

    public void OnTimerEnd()
    {
        animator.SetBool("attacking", false);
    }
}
