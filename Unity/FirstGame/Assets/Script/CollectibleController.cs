using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Collision Detection method
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("player collision a berry");
            // if the player's health is not full, destroy berry and hp+1
            if (player.IsMaxHealth())
            {
                player.ChangeHealth(1);
                Destroy(this.gameObject);
            }
        }
    }
}
