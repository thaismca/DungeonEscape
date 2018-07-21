using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour {

    [SerializeField]
    private float _speed = 3;


    // Update is called once per frame
    void Update()
    {
        //move right
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        //Destroy after 5 secs
        Destroy(this.gameObject, 5.0f);
    }

    //Detect collision with player and deal damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player _player = GameObject.Find("Player").GetComponent<Player>();
            _player.Damage();
            Destroy(this.gameObject);
        }
    }
}
