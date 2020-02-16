using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlacksmithUI : MonoBehaviour, IStaticPanel, IDynamicPanel
{

    #region Singleton
    public static BlacksmithUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is another instance of blacksmithUI");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject recipes;
    public GameObject materials;
    public GameObject craftingPanel;
    public GameObject recipePrefab;
    
    public ClickableObject craftButton;

    private ItemSlot[] recipesSlots;
    private Blacksmith currentBlacksmith;

    public ItemSlot resultSlot;
    private UIController uiController;

    private void Start()
    {
        uiController = UIController.instance;
        recipesSlots = materials.transform.GetComponentsInChildren<ItemSlot>();
    }

    public void SetBlacksmith(Blacksmith blacksmith)
    {
        this.currentBlacksmith = blacksmith;
    }

    public void TogglePanel(bool state)
    {
        UIController.instance.TogglePanel(craftingPanel, state);
    }

    public void ClearPanel()
    {
        throw new System.NotImplementedException();
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
        currentBlacksmith.choosenRecipe = recipe;
        craftButton.onLeft.AddListener(currentBlacksmith.Craft);
    }
}
