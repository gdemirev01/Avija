using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    private CharacterProps playerProps;

    [SerializeField]
    private Slider health;
    
    [SerializeField]
    private Slider mana;
    
    [SerializeField]
    private TextMeshProUGUI level;

    void Start()
    {
        playerProps = PlayerManager.Instance.player.GetComponent<CharacterProps>();
        InitializeInfo();
    }

    void Update()
    {
        UpdateInfo();
    }

    private void InitializeInfo()
    {
        health.minValue = 0;
        mana.minValue = 0;

        health.maxValue = playerProps.health;
        mana.maxValue = playerProps.mana;

        level.text = playerProps.level.ToString();
    }

    private void UpdateInfo()
    {
        health.value = playerProps.health;
        mana.value = playerProps.mana;

        level.text = playerProps.level.ToString();
    }
}
