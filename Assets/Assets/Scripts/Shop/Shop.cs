using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;

    //selected item
    public int selectedItem;
    public int selectedItemCost;

    //messages
    public Text message;

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

        //check for enough gems
        if (player.gems < selectedItemCost) 
        {
            message.text = "You don't have enough gems";
        }
        else
        {
            player.gems -= selectedItemCost;
            message.text = "Item purchased";
            UIManager.Instance.UpdateGemsCountHud(player.gems);
            UIManager.Instance.playerGemsCountText.text = "" + player.gems + "G";

            switch (selectedItem)
            {
                case 0: //flame sword
                    //create FlameSword function that increases the sword damage
                    break;
                case 1: //boots of flight
                    player.jumpForce *= 1.5f;
                    break;
                case 2: //key to the castle
                    GameManager.Instance.hasKeyToCastle = true;
                    break;
            }

        }
    }
}
