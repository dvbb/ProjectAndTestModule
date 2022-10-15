using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{
    public class DamageArea : MonoBehaviour
    {
        void OnTriggerStay2D(Collider2D collision)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ChangeHealth(-1);
            }
        }
    }
}
