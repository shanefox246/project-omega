using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{

    public static int money;
    public TMP_Text moneyText;

    public ShopItemScrObj[] shopItemsSO;
    public ShopTemplate[] shopPanels;

    public Button[] purchaseBtns;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "$ " + money.ToString();
        LoadPanels();
        CheckPurchasable();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + money.ToString();
    }
    public void CheckPurchasable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if(money >= shopItemsSO[i].basePrice)
                purchaseBtns[i].interactable = true;
            else
                purchaseBtns[i].interactable = false;
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleText.text = shopItemsSO[i].title;
            shopPanels[i].descriptionText.text = shopItemsSO[i].description;
            shopPanels[i].priceText.text = "$ " + shopItemsSO[i].basePrice.ToString();
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if( money >= shopItemsSO[btnNo].basePrice)
        {
            money = money - shopItemsSO[btnNo].basePrice;
            moneyText.text = "$ " + money.ToString();
            CheckPurchasable();
        }
    }
}
