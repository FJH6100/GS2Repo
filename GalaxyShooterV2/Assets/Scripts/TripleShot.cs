using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Powerup
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
                player.ActivateTripleShot();
            Destroy(this.gameObject);
        }
    }
}
