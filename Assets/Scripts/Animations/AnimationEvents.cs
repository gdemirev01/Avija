using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public DealDamage playerWeapon;

    public void EnableAttackTriggers()
    {
        playerWeapon.GetComponent<DealDamage>().ToggleTriggers(true);
    }

    public void DisableAttackTriggers()
    {
        playerWeapon.GetComponent<DealDamage>().ToggleTriggers(false);
    }
}
