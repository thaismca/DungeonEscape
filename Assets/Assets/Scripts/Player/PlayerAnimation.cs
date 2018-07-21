using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    Animator _playerAnimator;
    Animator _swordAnimator;

	// Use this for initialization
	void Start ()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _swordAnimator = transform.GetChild(1).GetComponent<Animator>();
	}


    public void RunAnimation(float horizontalInput)
    {
        _playerAnimator.SetFloat("move", Mathf.Abs(horizontalInput));
    }

    public void JumpAnimation(bool isJumping)
    {
        _playerAnimator.SetBool("jumping", isJumping);
    }

    public void AttackAnimation()
    {
        _playerAnimator.SetTrigger("attack");
        _swordAnimator.SetTrigger("swordAnimation");
    }

    public void Hit()
    {
        _playerAnimator.SetTrigger("hit");
    }

    public void Death()
    {
        _playerAnimator.SetTrigger("death");
    }
}
