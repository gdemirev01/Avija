using UnityEngine;

public class Swimming : MonoBehaviour
{
    new private Rigidbody rigidbody;
    public Animator animator;

    void Start()
    {
        rigidbody = transform.root.gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Water"))
        {
            StartSwimming();
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Water"))
        {
            StopSwimming();
        }
    }

    private void StartSwimming()
    {
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        animator.SetBool("inWater", true);
    }

    private void StopSwimming()
    {
        rigidbody.useGravity = true;
        animator.SetBool("inWater", false);
    }
}
