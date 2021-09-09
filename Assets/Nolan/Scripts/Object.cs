using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public string _name;
    public Sprite image;
    public int maxLvlKnowledge;
    public string descriptionlvl1;
    public string descriptionlvl2;
    public string descriptionlvl3;
    public int defaultPrice;
    //[HideInInspector]
    public int price;

    public int[] priceEachRound;
    [HideInInspector]
    public int mise;
    [HideInInspector]
    public bool playerProperty = false;

    public int sellingPrice;

    private void Start()
    {
        price = priceEachRound[0];
    }

    public void UpdatePrice(int i)
    {
        if(mise < priceEachRound[i])
        {
            price = priceEachRound[i];
            playerProperty = false;
        }
    }
}
