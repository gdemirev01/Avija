using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private CombatController combatController;

    private void Start()
    {
        combatController = CombatController.Instance;
    }

    public void EnableAttackTriggers()
    {
        combatController.ToggleTriggers(true);
    }

    public void DisableAttackTriggers()
    {
        combatController.ToggleTriggers(false);
    }

    public void ToggleWeapon(string state)
    {
        combatController.ToggleWeapon(state);
    }

    public void Cast()
    {
        combatController.CastSpell();
    }

    public void Shoot()
    {
        combatController.ShootSpell();
    }
}
