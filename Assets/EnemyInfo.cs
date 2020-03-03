using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : Singleton<EnemyInfo>
{
    private UIController uiController;

    private CharacterProps props;

    [SerializeField]
    private GameObject enemyInfoPanel;

    [SerializeField]
    private Slider health;

    [SerializeField]
    private TextMeshProUGUI level;

    private void Start()
    {
        uiController = UIController.Instance;
    }

    void Update()
    {
        if(props == null)
        {
            return;
        }

        UpdateInfo();
    }

    private void InitializeInfo()
    {
        health.minValue = 0;

        health.maxValue = props.health;

        level.text = "1";
    }

    private void UpdateInfo()
    {
        health.value = props.health;

        level.text = props.level.ToString();
    }

    public void SetEnemy(CharacterProps props)
    {
        this.props = props;
        InitializeInfo();
    }

    public void TogglePanel(bool state)
    {
        enemyInfoPanel.SetActive(state);
    }
}
