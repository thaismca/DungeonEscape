using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check for the Player
        if(other.tag == "Player")
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if(player != null)
            {
                //add the amount of gems to the Player
                player.AddGems(gems);
                Destroy(this.gameObject);
            }
        }
    }

}
