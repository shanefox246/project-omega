using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemScrObj : ScriptableObject
{
    // Start is called before the first frame update
    public string title;
    public string description;
    public int basePrice;
}
