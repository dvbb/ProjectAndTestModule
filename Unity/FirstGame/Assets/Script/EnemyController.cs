using System;
using System.Collections.Generic;
using System.Linq;
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

        private System.Random random => new System.Random();

        public void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            MoveActionChangeTimer = 2;
            RandomMoveDerection();
        }

        private void Update()
        {
            // move
            Vector2 position = rbody.position;
            position.x += moveDerection.x * Speed * Time.fixedDeltaTime;
            position.y += moveDerection.y * Speed * Time.fixedDeltaTime;
            rbody.MovePosition(position);

            // change move derection
            MoveActionChangeTimer -= Time.deltaTime;
            if (MoveActionChangeTimer < 0)
            {
                MoveActionChangeTimer = 3;
                RandomMoveDerection();
            }
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
