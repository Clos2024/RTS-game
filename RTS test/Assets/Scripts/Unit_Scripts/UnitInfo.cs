using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public float hungerMax;
    public float hunger;
    public float attackDmg;
    public float armor;
    public float HpMax;
    public float Hp;
    public float attackSpeed;
    public float attackRange;
    public string unitName;
    [SerializeField]
    private Item Weapon;
    [SerializeField]
    private Item Armor;

    public void starvation()
    {
        if (Hp > 0)
        {
            Hp--;
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
        Hp -= (dmg - armor);
    }

    public void eat(float amount)
    {
        hunger += amount;
        if (hunger > 100)
        {
            hunger = 100;
        }
    }

    public void equipWeapon(Item weapon)
    {
        if (weapon == null)
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
