using UnityEngine;

class Follow : MonoBehaviour {
    
    [SerializeField]
    private Transform target;
    
    private void Update()
    {
        transform.position = target.position;
    }
}