using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class EnemyController : MonoBehaviour
    {
        public float Speed = 2f;

        private Rigidbody2D rbody;
        private float MoveActionChangeTimer;
        private Vector2 moveDerection;
        private Animator animator;
        private bool isFixed;

        private System.Random random => new System.Random();

        public void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            MoveActionChangeTimer = 2;
            isFixed = false;
            RandomMoveDerection();
        }

        private void Update()
        {
            if (isFixed)
                return;

            // move
            Vector2 position = rbody.position;
            position.x += moveDerection.x * Speed * Time.fixedDeltaTime;
            position.y += moveDerection.y * Speed * Time.fixedDeltaTime;
            rbody.MovePosition(position);
            animator.SetFloat("MoveX", moveDerection.x);
            animator.SetFloat("MoveY", moveDerection.y);

            // change move derection
            MoveActionChangeTimer -= Time.deltaTime;
            if (MoveActionChangeTimer < 0)
            {
                MoveActionChangeTimer = 3;
                RandomMoveDerection();
            }
        }

        /// <summary>
        /// two rigid body collisions detect
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ChangeHealth(-1);
            }
        }

        public void FixRobot()
        {
            isFixed = true;
            rbody.simulated = false;    //disable rigid body 2d physical effects
            animator.SetTrigger("Fixed"); // play fixed anime
        }

        private void RandomMoveDerection()
        {
            int number = random.Next(4);
            switch (number)
            {
                case 0:
                    moveDerection = Vector2.up;
                    break;
                case 1:
                    moveDerection = Vector2.down;
                    break;
                case 2:
                    moveDerection = Vector2.left;
                    break;
                case 3:
                    moveDerection = Vector2.right;
                    break;
                default:
                    break;
            }
        }
    }
}
