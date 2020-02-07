using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithUI : MonoBehaviour
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

    private void Start()
    {
        recipesSlots = materials.transform.GetComponentsInChildren<ItemSlot>();
    }

    public void SetBlacksmith(Blacksmith blacksmith)
    {
        this.currentBlacksmith = blacksmith;
    }

    public void ToggleCraftingPanel(bool state)
    {
        UIController.instance.TogglePanel(craftingPanel, state);

    }

    public void UpdateRecipes()
    {
        foreach (Transform child in recipes.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentBlacksmith.recipes.Count; i++)
        {
            var recipeSlot = Instantiate(recipePrefab, recipes.transform, false);
            var slotScript = recipeSlot.GetComponent<ItemSlot>();

            ItemAmount item = new ItemAmount(currentBlacksmith.recipes[0], 1);
            slotScript.AddItem(item);
        }
    }

    public void LoadRecipe(Recipe recipe)
    {
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
