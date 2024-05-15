using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ShopMenu", menuName = "ScriptableObjects/New shop item",order = 1 )]
public class ShopItem : ScriptableObject
{
    public Sprite itemImg;
    public string title;
    public string description;
    public int price;
    public string currencyTxt;
    public Sprite currencyImg;


}
