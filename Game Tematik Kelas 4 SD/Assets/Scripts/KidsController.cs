using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraTransform;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private float speed;
    [SerializeField] private float gravity;

    void Update()
    {
        // Memproses input gerakan
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;

        // Gravitasi
        if (controller.isGrounded)
        {
            moveDirection.y = -0.1f; // Menarik karakter sedikit ke bawah untuk mencegah terbang
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime + moveDirection);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
