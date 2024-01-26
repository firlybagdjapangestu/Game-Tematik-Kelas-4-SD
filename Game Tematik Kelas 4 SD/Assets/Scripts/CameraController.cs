using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetFollow;
    public Vector3 offset;
    public float followSpeed = 5f;

    private void LateUpdate()
    {
        // Mengikuti posisi target
        Vector3 targetPosition = targetFollow.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        
    }
}
