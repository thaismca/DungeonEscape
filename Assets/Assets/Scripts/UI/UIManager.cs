using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI not instantiated");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    //Player Gems Count - Shop Panel
    public Text playerGemsCountText;
    public void OpenShop(int gemsCount)
    {
        playerGemsCountText.text = "" + gemsCount + "G";
    }

    //Item selection Image positioning
    public Image selectionImg;
    public void ShopSelectionPositioning(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    //Gems Count - HUD
    public Text hudGemsCountText;
    public void UpdateGemsCountHud(int count)
    {
        hudGemsCountText.text = "" + count;
    }

    //Player's life bar update
    public Image[] lifeUnits;
    public void UpdatePlayerLifeBar(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            {
                lifeUnits[i].enabled = false;
            }
        }
    }

}
