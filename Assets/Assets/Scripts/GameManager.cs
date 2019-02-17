using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Game Manager not instantiated");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    //Check Key To Castle
    public bool hasKeyToCastle = false;
}
