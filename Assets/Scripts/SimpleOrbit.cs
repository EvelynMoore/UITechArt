using UnityEngine;

public class OrbitTarget : MonoBehaviour
{
    [Tooltip("The target to orbit around.")]
    public Transform target;

    [Tooltip("Distance from the target.")]
    public float distance = 5f;

    [Tooltip("Orbit speed in degrees per second.")]
    public float orbitSpeed = 30f;

    [Tooltip("Vertical angle offset (degrees).")]
    public float verticalAngle = 20f;

    private float currentAngle = 0f;

    void Update()
    {
        if (target == null) return;

        currentAngle += orbitSpeed * Time.deltaTime;
        float radians = currentAngle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(radians) * distance,
            Mathf.Sin(verticalAngle * Mathf.Deg2Rad) * distance,
            Mathf.Sin(radians) * distance
        );

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}