using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    //Health - from IDamageable
    public int Health { get; set; }

    //Initialization
    public override void Init()
    {
        base.Init();
        //set Health
        Health = base.health;
    }

    //Damage method - from IDamageable
    public void Damage()
    {
        if (isDead)
        {
            return;
        }
        else
        {
            anim.SetTrigger("Hit");
            Health--;
            if (Health < 1)
            {
                //play Death animation
                anim.SetTrigger("Death");
                //Instantiate the reward and assing its value
                GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
                diamond.GetComponent<Diamond>().gems = this.gems;
                //set the Dead state to true
                isDead = true;
            }
        }
        
    }

}
