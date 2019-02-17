using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float _speed;
    public float jumpForce; //public so it can be changed from Shop when Player purchases Boots Of Flight
    [SerializeField]
    private bool _resetJump;
    public int gems = 0;

    //check states
    private bool _isDead = false;
    private bool _grounded;

    //layerMask
    [SerializeField]
    LayerMask _groundLayer;

    //get handle to Rigidbody2D
    private Rigidbody2D _playerRb;
    //get handle to PlayerAnimation Script
    private PlayerAnimation _playerAnimation;
    //get handle to the Animator Component
    private Animator _anim;
    //get handle to the Player Sprite Renderer
    private SpriteRenderer _playerSprite;
    //get handle to the Sword_Arc Sprite Renderer
    private SpriteRenderer _swordArcSprite;

    //Implementing IDamageable Health Property
    public int Health { get; set; }

    void Start ()
    {
        //assign the handle to Rigidbody2D
        _playerRb = GetComponent<Rigidbody2D>();
        //assign the handle to PlayerAnimation Script
        _playerAnimation = GetComponent<PlayerAnimation>();
        //assing the handle to Animator Component
        _anim = GetComponentInChildren<Animator>();
        //assign the handle to Player Sprite Renderer
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        //assing the handle to the Sword_Arc Sprite Renderer
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        //Setting Health
        Health = 4;
        //Setting Gems
        UIManager.Instance.UpdateGemsCountHud(gems);
    }

    // Update is called once per frame
    void Update ()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")||
            _anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
            _isDead)
        {
            _playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            _playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerMovement();
            Attack();
        }
	}

    //Face Left / Right
    void FlipSprite(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            //player flip
            _playerSprite.flipX = false;

            //sword arc effect flip
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 newLocalPos = _swordArcSprite.transform.localPosition;
            newLocalPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newLocalPos;
        }
        else if (horizontalInput < 0)
        {
            //player flip
            _playerSprite.flipX = true;

            //sword arc effect flip
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newLocalPos = _swordArcSprite.transform.localPosition;
            newLocalPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newLocalPos; 
        }
    }

    //Movements
    void PlayerMovement()
    {
        _grounded = IsGrounded();
        float horizontalInput = Input.GetAxisRaw("Horizontal") * _speed;

        FlipSprite(horizontalInput);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce);
            //Animation Idle -> Jump
            _playerAnimation.JumpAnimation(true);
            StartCoroutine("ResetJumpRoutine");
        }

        //Move Left / Right
        _playerRb.velocity = new Vector2(horizontalInput, _playerRb.velocity.y);

        //Animation Idle / Run
        _playerAnimation.RunAnimation(horizontalInput);
    }

    //Check if the player is grounded
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            if(_resetJump == false)
            {
                //Animation Jump -> Idle
                _playerAnimation.JumpAnimation(false);
                return true;
            }
        }

        return false;
    }

    //cool down system to avoid spamming the jump
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    //Attack
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded())
        {
            //Animation Idle -> Attack
            _playerAnimation.AttackAnimation();
        }
    }

    //Damage (from IDamageable)
    public void Damage()
    {
        Health--;
        UIManager.Instance.UpdatePlayerLifeBar(Health);
        if (Health < 1)
        {
            _playerAnimation.Death();
            _isDead = true;
            return;
        }
        _playerAnimation.Hit();
    }

    //Add collected Gems
    public void AddGems(int count)
    {
        gems += count;
        UIManager.Instance.UpdateGemsCountHud(gems);
    }

}
