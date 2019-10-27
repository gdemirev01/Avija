using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveHit : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void receiveHit(GameObject other)
    {
        if (other.tag.Equals("Weapon"))
        {
            //lower health
            animator.SetTrigger("getHitted");
        }
    }
}
