using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets
{
    public class Entity : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Animator animator;

        protected bool facingRight = true;
        protected int facingDirection = 1;
        protected float xInput;

        [Header("collision info")]
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected float groundCheckDistance;
        [SerializeField] protected LayerMask whatIsGround;

        [Header("MoveInfo")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float moveSpeed;
        [SerializeField] protected bool isMoving = false;

        protected bool isGrounded;

        public Entity()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            jumpForce = 5;
            moveSpeed = 5;
        }

        #region Collision
        protected void OnDrawGizmos()
        {
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        }

        protected void CollisionChecks()
        {
            // Raycast: 从[transform.position]发射一根射线 
            // 方向: Vector2.down
            // 发射距离: groundCheckDistance
            // 判定对象: whatIsGround(即为某个layout，当前unity中设定为 ground 地板层) 接触到则返回 true
            isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        }
        #endregion

        #region Move
        protected void Movement(float dashTime, float dashSpeed)
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

        protected void Jump()
        {
            if (isGrounded)
                rb.velocity = new Vector2(xInput, jumpForce);
        }
        #endregion
    }
}
