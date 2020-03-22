using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private string[] combatTriggers;

    void AbortAnimations()
    {

    }

    public void ReceiveHit()
    {
        animator.SetTrigger("getHit");
    }
}
