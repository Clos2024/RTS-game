using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingMenuHandler : MonoBehaviour
{
    public GameObject[] menus;
    private int i;

    void Awake()
    {
        i = 0;

        if (menus[0] != null)
        {
            foreach (var game in menus)
            {
                game.SetActive(false);
            }
            menus[0].SetActive(true);
        }
        menus[i].SetActive(true);
    }
    public void previous()
    {
        menus[i].SetActive(false);
        if (i == 0)
        {
            i = menus.Length - 1;
        }
        else
        {
            i--;
        }
        menus[i].SetActive(true);
    }
    public void next()
    {
        menus[i].SetActive(false);
        if (i == menus.Length - 1)
        {
            i = 0;
        }
        else
        {
            i++;
        }
        menus[i].SetActive(true);
    }
}
