using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlacksmithUI : Singleton<BlacksmithUI>, IStaticPanel, IDynamicPanel
{
    [SerializeField]
    private GameObject recipes;
    [SerializeField]
    private GameObject materials;
    [SerializeField]
    private GameObject craftingPanel;
    [SerializeField]
    private GameObject recipePrefab;
    [SerializeField]
    private ClickableObject craftButton;
    [SerializeField]
    private ItemSlot resultSlot;

    private Blacksmith currentBlacksmith;
    private UIController uiController;

    private ItemSlot[] recipesSlots;

    private void Start()
    {
        uiController = UIController.Instance;
        recipesSlots = materials.transform.GetComponentsInChildren<ItemSlot>();
    }

    public void SetBlacksmith(Blacksmith blacksmith)
    {
        this.currentBlacksmith = blacksmith;
    }

    public void TogglePanel(bool state)
    {
        UIController.Instance.TogglePanel(craftingPanel, state);
    }

    public void ClearPanel()
    {
        foreach (ItemSlot slot in recipesSlots)
        {
            slot.ClearSlot();
        }
    }

    public void UpdatePanel()
    {
        ItemSlot[] slots = uiController.CreateSlots<ItemSlot>(recipes, recipePrefab, currentBlacksmith.recipes.Count);
        uiController.LoadItemsToSlots(slots, currentBlacksmith.recipes.Select(r => new ItemAmount(r, 1)).ToArray());
    }

    public void LoadInfoInPanel(ScriptableObject info)
    {
        Recipe recipe = info as Recipe;

        for (int i = 0; i < recipesSlots.Length; i++)
        {
            if (i < recipesSlots.Length)
            {
                recipesSlots[i].AddItem(recipe.materials[i]);
                resultSlot.AddItem(recipe.resultItem);
            }
            else
            {
                recipesSlots[i].ClearSlot();
                resultSlot.ClearSlot();
            }
        }
        currentBlacksmith.chosenRecipe = recipe;
        craftButton.onLeft.AddListener(currentBlacksmith.Craft);
    }
}
