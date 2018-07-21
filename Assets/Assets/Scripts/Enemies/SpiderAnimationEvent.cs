using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour {

    //reference to the Spider Component
    Spider _spider;

    private void Start()
    {
        //assing handle to Spider
        _spider = GetComponentInParent<Spider>();
    }

    //call Attack method from Spider
    void FireAcid()
    {
        if(_spider != null)
        {
            _spider.Attack();
        }

    }
}
