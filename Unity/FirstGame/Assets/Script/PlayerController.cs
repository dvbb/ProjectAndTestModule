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

    private float invincibleTime = 2f;
    private float invincibleTimer;
    private bool isInvincible;
    private Animator animator;

    // Player Directions
    private Vector2 playerDirection = new Vector2(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = 5;
        invincibleTimer = 0;
        isInvincible = false;
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        // Arrow keys to control movement
        float moveX = Input.GetAxisRaw("Horizontal");   // [A] -1 [D]  1
        float moveY = Input.GetAxisRaw("Vertical");     // [W] 1  [S] -1

        // Set player direction with the arrow keys
        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveX != 0 || moveY != 0)
        {
            playerDirection = moveVector;
            animator.SetFloat("Look X", playerDirection.x);
            animator.SetFloat("Look Y", playerDirection.y);
        }
        animator.SetFloat("Speed", moveVector.magnitude);

        Vector2 position = rbody.position;
        //position.x += moveX * Speed * Time.fixedDeltaTime;
        //position.y += moveY * Speed * Time.fixedDeltaTime;
        position += moveVector * Speed * Time.fixedDeltaTime;
        rbody.MovePosition(position);

        // update invincible time
        invincibleTimer -= Time.deltaTime;
        if (isInvincible && invincibleTimer <= 0)
        {
            isInvincible = false;
        }
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
        // if player is not invincible then deal damage and reset invincible time
        if (ammount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            CurrentHealth = Mathf.Clamp(CurrentHealth + ammount, 0, MaxHealth);
            Debug.Log($"HP:{this.GetCureentHealth()}/{this.GetMaxHealth()}...");
            Debug.Log($"reset invicible...");
            isInvincible = true;
            invincibleTimer = invincibleTime;
            return;
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth + ammount, 0, MaxHealth);
        Debug.Log($"HP:{this.GetCureentHealth()}/{this.GetMaxHealth()}...");
    }
}
