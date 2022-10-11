using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    private float MaxHealth = 8;
    private float CurrentHealth;

    public Rigidbody2D rbody;

    public float GetCureentHealth() => CurrentHealth;
    public float GetMaxHealth() => MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = 5;
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


    public bool IsMaxHealth()
    {
        if (this.MaxHealth == this.CurrentHealth)
        {
            return false;
        }
        return true;
    }

    public void ChangeHealth(float ammount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + ammount, 0, MaxHealth);
        Debug.Log($"HP:{this.GetCureentHealth()}/{this.GetMaxHealth()}...");
    }
}
