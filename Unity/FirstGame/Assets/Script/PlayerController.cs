using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;

    public Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        // Horizontal movement to the right
        //transform.Translate(transform.right * Speed * Time.deltaTime);

        // Arror keys to control movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 position = rbody.position;
        position.x += moveX * Speed * Time.fixedDeltaTime;
        position.y += moveY * Speed * Time.fixedDeltaTime;
        rbody.MovePosition(position);
    }
}
