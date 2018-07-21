using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    //Health from IDamageable
    public int Health { get; set; }

    //Acid Prefab
    [SerializeField]
    private GameObject _acid;

    //Initialization
    public override void Init()
    {
        base.Init();
        //set Health
        Health = base.health;
    }

    //override method update
    public override void Update()
    {
        //keep calculating the distance between player and enemy
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);

        //if the player is on sight, attack
        if (playerDistance < 6)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
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

    //override Move method - the spider won't move
    public override void Move()
    {
       //stay still
    }

    //Instantiate the acid prefab
    //It's going to be called fron SpiderAnimationEvent
    public void Attack()
    {
        Instantiate(_acid, transform.position + new Vector3(-0.487f, -0.051f, 0), Quaternion.identity);
    }

}
