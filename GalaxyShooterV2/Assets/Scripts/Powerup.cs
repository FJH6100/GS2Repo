using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{ 
    private enum PowerupType
    {
        Shield,
        Speed,
        TripleShot,
        AmmoRefill
    };
    [SerializeField]
    private float _powerupSpeed = 3f;
    [SerializeField]
    private PowerupType _type;
       
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerupSpeed);
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            { 
                switch(_type)
                {
                    case PowerupType.Shield:
                        player.ActivateShield();
                        break;
                    case PowerupType.Speed:
                        player.ActivateSpeed();
                        break;
                    case PowerupType.TripleShot:
                        player.ActivateTripleShot();
                        break;
                    case PowerupType.AmmoRefill:
                        player.RefillAmmo();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
