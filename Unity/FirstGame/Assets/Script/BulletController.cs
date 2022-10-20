using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    internal class BulletController : MonoBehaviour
    {
        private Rigidbody2D _rbody;

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            Destroy(this.gameObject, 2f);
        }

        private void Update()
        {

        }

        public void Move(Vector2 moveDerection, float moveForce)
        {
            _rbody.AddForce(moveDerection * moveForce);
        }

        /// <summary>
        /// collision detection
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                Debug.Log("hitting the enemy");
                enemyController.FixRobot();
            }
            Destroy(this.gameObject);
        }
    }
}
