using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Transform player;

    private float rotationSpeed = 0.5f;
    private float zoomSpeed = 4f;

    private Vector3 direction;
    private float inputX;
    private float inputY;

    public float minDistance = 5.0f;
    public float maxDistance = 30.0f;
    private float distance;


    public float rayDistance;

    void Awake()
    {
        direction = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        target.rotation = target.rotation;
    }

    private void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }

    private void CamControl()
    {
        if (InputController.Instance.GetPointerState())
        {
            return;
        }

        inputX += Input.GetAxis("Mouse X") * rotationSpeed;
        inputY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        inputY = Mathf.Clamp(inputY, -35, 60);


        // camera is always rotated in targe's direction
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // target is rotated with the mouse coords
        target.rotation = Quaternion.Euler(inputY, inputX, 0);
    }

    private void ViewObstructed()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(direction * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            distance = Mathf.Clamp((hit.distance * rayDistance), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, direction * distance, Time.deltaTime * zoomSpeed);
    }
}