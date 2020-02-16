using UnityEngine;

public class LevelController : MonoBehaviour
{
    private CharacterProps playerProps;

    [SerializeField]
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
