using UnityEngine;

public class CharacterProps : MonoBehaviour 
{
    new public string name;
    public float health;
    public float mana;
    public float exp;
    public int level;
    public int coins;

    public int damage;
    public int armor;

    public string lines;

    private bool loadedSave = false;

    private void Update()
    {
        if(tag.Equals("Player") && !loadedSave)
        {
            SaveSystem.Instance.LoadSave();
            loadedSave = true;
        }
    }
}
