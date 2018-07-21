using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int gems;
    protected bool isDead = false;

    //Movement
    [SerializeField]
    protected Transform pointA, pointB;
    private Vector3 _currentTarget;

    //Combat Mode
    [SerializeField]
    protected float playerDistance;

    //reference to the Animator Component
    protected Animator anim;
    //reference to the Sprite Renderer Component
    private SpriteRenderer _enemySprite;
    //reference to the Player
    protected Player player;

    //Diamond Prefab
    [SerializeField]
    protected GameObject diamondPrefab;


    // Use this for initialization
    public virtual void Init()
    {
        //assing components
        anim = GetComponentInChildren<Animator>();
        _enemySprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(!isDead)
        {
            //keep calculating the distance between player and enemy
            playerDistance = Vector3.Distance(this.transform.position, player.transform.position);

            if (playerDistance > 2)
            {
                anim.SetBool("Combat", false);

                // if the animation that is currently playing isn't Walk, do nothing
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    return;
                }

                Move();
            }
            else if (playerDistance <= 2)
            {
                CombatMode();
            }

        }

    }

    // Move the character
    public virtual void Move()
    {
        // Flip the sprite in the correct direction
        FlipSprite();

        // if the current position equals the pointA way point
        if (transform.position == pointA.position)
        {
            // set the current target to the pointB way point and play the Idle animation once
            _currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        // if the current position equals the pointB way point
        else if (transform.position == pointB.position)
        {
            // set the current target to the pointA way point and play the Idle animation once
            _currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        // move the character from the current position towards to the target way point
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);

    }

    // Flip the sprite in the correct direction using the current target as reference
    public void FlipSprite()
    {
        if (_currentTarget == pointA.position)
            _enemySprite.flipX = true;
        else if (_currentTarget == pointB.position)
            _enemySprite.flipX = false;
    }

    //Activate the Combat Mode
    public void CombatMode()
    {
        anim.SetBool("Combat", true);

        //Flip the sprite to the player direction
        if (this.transform.position.x > player.transform.position.x)
            _enemySprite.flipX = true;
        else if (this.transform.position.x < player.transform.position.x)
            _enemySprite.flipX = false;

    }

}
