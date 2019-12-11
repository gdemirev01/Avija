using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public CharacterProps playerProps;
    private LevelSystemUI levelSystemUI;

    private float expRequired;
    private float expIncrease;
    // Start is called before the first frame update

    private void Awake()
    {
        expRequired = 1000f;
        expIncrease = 1.2f;
    }

    void Start()
    {
        levelSystemUI = GetComponent<LevelSystemUI>();
    }

    public void AddExp(float value)
    {
        playerProps.exp += value;

        while(playerProps.exp >= expRequired)
        {
            playerProps.level++;

            levelSystemUI.SetExpBarLimits(expRequired, expRequired * expIncrease);
            levelSystemUI.SetExpBarValue(playerProps.exp);

            expRequired *= expIncrease;
        }
    }
}
