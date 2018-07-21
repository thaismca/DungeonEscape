using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI not istantiated");
            }

            return _instance;
        }
    }

    //Player Gems Count
    public Text playerGemsCountText;
    public void OpenShop(int gemsCount)
    {
        playerGemsCountText.text = "" + gemsCount + "G";
    }

    //Selection Image Positioning
    public Image selectionImg;
    public void ShopSelectionPositioning(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    private void Awake()
    {
        _instance = this;
    }
}
