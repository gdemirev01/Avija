using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void EndAttack()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().EndAttack();
    }

    public void EnableAttackTriggers()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ToggleTriggers(true);
    }

    public void DisableAttackTriggers()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ToggleTriggers(false);
    }

    public void ResetComboStreak()
    {
        GameObject.Find("PlayerWeapon").GetComponent<ComboSystem>().ResetStreak();
    }

    public void ResetAttackTriggger()
    {
        GameObject.Find("PlayerWeapon").GetComponent<DealDamage>().ResetAttackTrigger();
    }
}
