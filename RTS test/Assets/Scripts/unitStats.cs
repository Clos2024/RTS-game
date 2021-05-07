using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitStats : MonoBehaviour
{
    public float hunger;
    public float attack;
    public float armor;
    public float health;
    public float attackSpeed;
    public float attackRange;
    public string unitName;
    [SerializeField]
    private Item Weapon;
    [SerializeField]
    private Item Armor;

    public void starvation()
    {
        if (health > 0)
        {
            health--;
        }
    }

    public void metabolism()
    {
        if (hunger > 0)
        {
            hunger--;
        }
    }

    public void takeDamage(float dmg)
    {
        health -= (dmg - armor);
    }

    public void eat(float amount)
    {
        hunger += amount;
        if(hunger > 100)
        {
            hunger = 100;
        }
    }

    public void equipWeapon(Item weapon)
    {
        if(weapon == null)
        {
            this.Weapon = weapon;
        }
    }

    public void equipArmor(Item armor)
    {
        if (armor == null)
        {
            Armor = armor;
        }
    }
}
