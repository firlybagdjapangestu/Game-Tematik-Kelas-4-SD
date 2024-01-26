using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public int answerPlayer;
    public int scorePlayer;
    public float speed;
    public float turnSmoothTime;
    public float turnSmoothVelocity;
    public Transform cameraTransform;
    public FixedJoystick joystick;
    public Button jumpButton; // Menggunakan Button dari UI UnityEngine

    public float jumpForce = 8.0f;
    public float gravity = 30.0f;

    private Vector3 moveDirection = Vector3.zero;

    public QuestionManager questionManager;
    public int score;
    public bool hasAnswer;

    public Animator animator;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        jumpButton.onClick.AddListener(Jump);
        animator.SetBool("isSwiming", false);
    }

    void Update()
    {
        // Memproses input gerakan
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Jump()
    {      
        if (controller.isGrounded)
        {
            animator.SetBool("isSwiming", false);
            animator.SetTrigger("isJumping");
            moveDirection.y = 0f;
            moveDirection.y = jumpForce;
        }
        moveDirection.y -= gravity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WaterGround"))
        {
            speed = 5;
            hasAnswer = false;
            animator.SetBool("isSwiming", true);
        }
        if (collision.gameObject.CompareTag("PlaneGround"))
        {
            speed = 10;
            hasAnswer = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlaneOption") && !hasAnswer)
        {
            AnswerPlane answerPlane = other.gameObject.GetComponent<AnswerPlane>();
            if (answerPlane != null && questionManager.timeAnswering)
            {
                print(answerPlayer);
                answerPlayer = answerPlane.planeOption;
                if (answerPlayer == questionManager.questions[questionManager.questionIndex].correctOptionIndex)
                {
                    score++;
                    hasAnswer = true;
                    scoreText.text = score.ToString();
                    Debug.Log("Benar");
                }
                else
                {
                    Debug.Log("salah");
                }
            }
        }
    }
}
