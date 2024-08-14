using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("DashInfo")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashColdDown;
    private float dashColdDownTimer;

    public Player() : base()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        dashSpeed = 15;
        dashColdDown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Movement(dashTime, dashSpeed);
        CheckInput();
        CollisionChecks();

        dashTime -= Time.deltaTime;
        dashColdDownTimer -= Time.deltaTime;

        FlipContraller();
        AnimatorControllers();
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
}