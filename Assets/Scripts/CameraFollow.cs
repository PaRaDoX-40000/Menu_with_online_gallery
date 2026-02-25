using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       
    public float smoothSpeed = 0.125f; 
    public float verticalOffset = 2f;  

    private float highestY; 

    void Start()
    {
        if (target != null)
        {
            highestY = transform.position.y;
        }
    }

    
    void LateUpdate()
    {
        if (target == null) return;
        float desiredY = target.position.y + verticalOffset;
        if (desiredY > highestY)
        {
            highestY = desiredY;
        }      
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x, highestY, transform.position.z),
            smoothSpeed
        );
        transform.position = smoothedPosition;
    }
}