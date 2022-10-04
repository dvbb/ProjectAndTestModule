using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        System.Console.WriteLine(Speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement to the right
        //transform.Translate(transform.right * Speed * Time.deltaTime);

        // Arror keys to control movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;
        position.x += moveX * Speed * Time.deltaTime;
        position.y += moveY * Speed * Time.deltaTime;
    }
}
