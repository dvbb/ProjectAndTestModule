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
        transform.Translate(transform.right * Speed * Time.deltaTime);
    }
}
