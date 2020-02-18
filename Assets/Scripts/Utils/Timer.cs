using UnityEngine;

public class Timer : MonoBehaviour
{

    public float interval;

    private float timerCounter;

    public bool timerRunning = false;

    public delegate void OnTimerEnd();
    public OnTimerEnd onTimerEnd;

    public delegate void OnTimerRestart();
    public OnTimerRestart onTimerRestart;

    void Start()
    {
        timerCounter = interval;
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
        timerCounter -= Time.deltaTime;
        if (timerCounter < 0)
        {
            timerRunning = false;
            timerCounter = interval;
            onTimerEnd.Invoke();
        }
    }

    public void RestartTimer()
    {
        timerRunning = true;
        timerCounter = interval;
        onTimerRestart.Invoke();
    }
}