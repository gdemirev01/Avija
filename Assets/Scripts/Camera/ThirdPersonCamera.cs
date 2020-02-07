using System.Linq;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Transform player;

    float rotationSpeed = 0.5f;
    float zoomSpeed = 4f;

    Vector3 direction;
    float mouseX, mouseY;

    public float minDistance = 5.0f;
    public float maxDistance = 30.0f;
    private float distance;


    public float rayDistance;

    void Awake()
    {
        direction = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    private void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }

    void CamControl()
    {
        if(Cursor.visible && Cursor.lockState != CursorLockMode.Locked) { return; }

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        var targetRotation = Quaternion.LookRotation(target.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }

    void ViewObstructed()
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