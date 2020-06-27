﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public int indexItem;
    public int amount;
    public TextMeshProUGUI infoText;

    private string itemName;
    private string itemType;
    private string itemRarity;
    private int itemPrice;

    public void InitalizeSelf(int currentIndex, PlayerManager player)
    {
        indexItem = currentIndex;
        if (currentIndex < player.invetory.Count)
        {
            indexItem = currentIndex;
            amount = 0;
            FillValues(player);
        }
        else
        {
            indexItem = -1;
            amount = -1;
        }

        //day 2 added ----------------------------------------------------------------------------
        if(infoText == null)
        {
            infoText = GameObject.FindWithTag("Info").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateSelf(PlayerManager player)
    {
        if (indexItem > -1)
        {
            amount = player.invetory[indexItem].itemAmount;
            GetComponentInChildren<Image>().sprite = player.lt.lootItems[indexItem].sprite;
            GetComponentInChildren<Image>().color = player.lt.lootItems[indexItem].color;
            FillValues(player);
            GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
        }
        else
        {
            GetComponentInChildren<Image>().enabled = false;
            GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        
    }

    void FillValues(PlayerManager player)
    {
        itemName = player.lt.lootItems[indexItem].itemName;
        itemType = player.lt.lootItems[indexItem].itemType.ToString();
        itemRarity = player.lt.lootItems[indexItem].rarity.ToString();
        itemPrice = player.lt.lootItems[indexItem].price;
}

    public void UpdateInfo()
    {
        infoText.text = "Name: " + itemName +
                        "\nAmount: " + amount.ToString() +
                        "\nType: " + itemType +
                        $"\nRarity: {itemRarity}%" +
                        "\nPrice Per: " + itemPrice.ToString() +
                        "\nTotal Price: " + (itemPrice * amount).ToString();
    }
}
