using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyaer : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;

    private float xInput;
    [SerializeField] private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        jumpForce = 5;
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();
        AnimatorControllers();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Jump() 
    {
        Debug.Log("jump!");
        rb.velocity = new Vector2(xInput, jumpForce);
    }

    private void AnimatorControllers()
    {
        isMoving = rb.velocity.x != 0;
        animator.SetBool("isMoving", isMoving);
    }
}
