using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    //variable to check if any damage can be caused (cooldown system)
    private bool _canDamage = true;

    //Detect collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        //if other implements IDamageable, cause damage
        if (hit != null)
        {
            if (_canDamage && hit.Health >= 1)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine("CanDamage");
            }
        }
    }

    //cooldown system
    private IEnumerator CanDamage()
    {
        yield return new WaitForSeconds(0.7f);
        _canDamage = true;
    }
}
