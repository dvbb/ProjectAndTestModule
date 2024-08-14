using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plyaer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;

    [Header("DashInfo")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;

    [SerializeField] private float dashColdDown;
    private float dashColdDownTimer;

    private bool facingRight = true;
    private int facingDirection = 1;
    private float xInput;
    [SerializeField] private bool isMoving = false;

    [Header("collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        jumpForce = 5;
        moveSpeed = 5;
        dashSpeed = 15;
        dashColdDown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();

        dashTime -= Time.deltaTime;
        dashColdDownTimer -= Time.deltaTime;

        FlipContraller();
        AnimatorControllers();
    }

    private void CollisionChecks()
    {
        // Raycast: 从[transform.position]发射一根射线 
        // 方向: Vector2.down
        // 发射距离: groundCheckDistance
        // 判定对象: whatIsGround(即为某个layout，当前unity中设定为 ground 地板层) 接触到则返回 true
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void DashAbility()
    {
        if (dashColdDownTimer < 0)
        {
            dashColdDownTimer = dashColdDown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(xInput, jumpForce);
    }

    private void AnimatorControllers()
    {
        isMoving = rb.velocity.x != 0;
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isDashing", dashTime > 0);
    }

    private void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipContraller()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}