using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardScroller : MonoBehaviour
{
    public GameObject[] cards;
    public int counter = 0;

    void Awake()
    {
        counter = 0;

        if(cards[0] != null)
        {
            foreach (var game in cards)
            {
                game.SetActive(false);
            }
            cards[0].SetActive(true);
        }
        cards[counter].SetActive(true);
    }
    public void previous()
    {
        cards[counter].SetActive(false);
        if (counter == 0)
        {
            counter = cards.Length-1;
        }
        else
        {
            counter--;
        }
        cards[counter].SetActive(true);
    }
    public void next()
    {
        cards[counter].SetActive(false);
        if (counter == cards.Length-1)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        cards[counter].SetActive(true);
    }
}
