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
    public Item Weapon;
    [SerializeField]
    public Item Armor;

    public Unit unitScript;

    private void Awake()
    {
        unitScript = transform.GetComponent<Unit>();
    }
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

    public void takeDamage(float dmg, GameObject go)
    {
        if(unitScript.target == null)
        {
            if(go.GetInstanceID() != transform.gameObject.GetInstanceID())
                unitScript.target = go;
        }
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
        Debug.Log("Applying Weapon");
        Weapon = weapon;
        attackDmg += weapon.Dmg;
    }

    public void equipArmor(Item armor)
    {
        Debug.Log("Applying Armor");
        Armor = armor;
        this.armor += armor.armor;
    }
#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Color c = new Color(0, 0, 0.7f, 0.1f);
        UnityEditor.Handles.color = c;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, Vector3.forward, 360, attackRange);
    }
#endif
}
