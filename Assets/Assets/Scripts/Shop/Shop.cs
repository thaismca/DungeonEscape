﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;

    //selected item
    public int selectedItem;
    public int selectedItemCost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                UIManager.Instance.OpenShop(player.gems);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        selectedItem = item;

        switch (item)
        {
            case 0: //flame sword
                UIManager.Instance.ShopSelectionPositioning(255);
                selectedItemCost = 200;
                break;
            case 1: //boots of flight
                UIManager.Instance.ShopSelectionPositioning(153);
                selectedItemCost = 400;
                break;
            case 2: //key to the castle
                UIManager.Instance.ShopSelectionPositioning(58);
                selectedItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        if(player.gems < selectedItemCost)
        {
            Debug.Log("Not enough gems");
        }
        else
        {
            player.gems -= selectedItemCost;
            Debug.Log("Done");
        }
    }
}
