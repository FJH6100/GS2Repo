using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerupSpeed);
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}
