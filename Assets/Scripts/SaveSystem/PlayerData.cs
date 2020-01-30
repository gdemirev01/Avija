using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float exp;
    public int level;
    public int coins;
    public List<int> items;
    public List<int> equipment;
    public float[] position;

    public int damage;
    public int armor;

    public PlayerData(float health, float exp, int level, int coins, List<int> items, List<int> equipment, float[] position, int damage, int armor)
    {
        this.health = health;
        this.exp = exp;
        this.level = level;
        this.coins = coins;
        this.items = items;
        this.equipment = equipment;
        this.position = position;
        this.damage = damage;
        this.armor = armor;
    }
}
