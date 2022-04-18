using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
                player.ActivateShield();
            Destroy(this.gameObject);
        }
    }
}
