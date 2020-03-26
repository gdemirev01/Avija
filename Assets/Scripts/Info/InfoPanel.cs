using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    private CharacterProps props;

    [SerializeField]
    private Slider health;
    
    [SerializeField]
    private Slider mana;
    
    [SerializeField]
    private TextMeshProUGUI level;

    void Start()
    {
        props = PlayerManager.Instance.player.GetComponent<CharacterProps>();
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

        health.maxValue = props.maxHealth;
        mana.maxValue = props.mana;

        level.text = "1";
    }

    private void UpdateInfo()
    {
        health.value = props.currentHealth;
        mana.value = props.mana;

        level.text = props.level.ToString();
    }
}
