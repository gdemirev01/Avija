using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private bool enableAttackTriggers = false;

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (enableAttackTriggers)
        {
            if ((gameObject.tag.Equals("Player") && other.tag.Equals("Enemy")) ||
                (gameObject.tag.Equals("Enemy") && other.tag.Equals("PlayerCollider")))
            {
                animator.ResetTrigger("getHit");
                other.gameObject.GetComponent<ReceiveHit>().GetHit(gameObject);
            }
        }
    }

    public void Attack()
    {
        animator.SetBool("attacking", true);
    }

    public void EndAttack()
    {
        animator.SetBool("attacking", false);
    }

    public void ToggleTriggers(bool state)
    {
        enableAttackTriggers = state;
    }
}
