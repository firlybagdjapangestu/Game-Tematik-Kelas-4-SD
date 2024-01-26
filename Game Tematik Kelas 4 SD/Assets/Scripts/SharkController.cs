using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public Transform targetToChase; // Objek yang akan dikejar
    public float movementSpeed = 5.0f; // Kecepatan pergerakan objek yang mengejar
    public float maxRotationX = 60.0f; // Batas rotasi sumbu X

    private float startYPosition; // Menyimpan posisi Y awal

    private void Start()
    {
        // Simpan posisi Y awal saat inisialisasi
        startYPosition = transform.position.y;
    }

    private void Update()
    {
        if (targetToChase != null)
        {
            MoveTowardsTarget();
            RotateTowardsTarget();
            ClampRotationX();
            LockYPosition();
        }
        else
        {
            Debug.LogWarning("Tidak ada target yang ditetapkan untuk dikejar.");
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 targetDirection = targetToChase.position - transform.position;
        Vector3 normalizedDirection = targetDirection.normalized;
        Vector3 movement = normalizedDirection * movementSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + movement;
        newPosition.z = 0;
        newPosition.y = startYPosition; // Kunci posisi Y

        transform.position = Vector3.MoveTowards(transform.position, targetToChase.position, movement.magnitude);
    }

    void RotateTowardsTarget()
    {
        Vector3 targetDirection = targetToChase.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
    }

    void ClampRotationX()
    {
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.x = Mathf.Clamp(eulerRotation.x, -maxRotationX, maxRotationX);
        transform.rotation = Quaternion.Euler(eulerRotation);
    }

    void LockYPosition()
    {
        Vector3 lockedPosition = transform.position;
        lockedPosition.y = startYPosition;
        transform.position = lockedPosition;
    }
}
