using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private CharacterProps playerProps;

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
        playerProps = GameObject.Find("Player").GetComponent<CharacterProps>();
    }

    public void AddExp(float value)
    {
        playerProps.exp += value;

        while(playerProps.exp >= expRequired)
        {
            playerProps.level++;
            expRequired *= expIncrease;
        }
    }
}
