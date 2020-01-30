using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private CharacterProps playerProps;
    private LevelSystemUI levelSystemUI;

    private float expRequired;
    private float expIncrease;

    private void Awake()
    {
        expRequired = 1000f;
        expIncrease = 1.2f;
    }

    void Start()
    {
        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();
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
