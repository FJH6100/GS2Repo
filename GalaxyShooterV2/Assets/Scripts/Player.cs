using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .1f;
    private int _lives = 3;
    private float _canFire;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShot;
    private bool _tripleShotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _canFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (_tripleShotActive)
                Instantiate(_tripleShot, transform.position + Vector3.up, Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives--;
        if (_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActivatePowerup()
    {
        StartCoroutine(PowerupActive());
    }

    IEnumerator PowerupActive()
    {
        _tripleShotActive = true;
        yield return new WaitForSeconds(5f);
        _tripleShotActive = false;
    }

    void PlayerMovement()
    {
        float horizonal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizonal, vertical, 0);
        transform.Translate(direction * Time.deltaTime * speed);
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(10f, transform.position.y, 0);
        }
        else if (transform.position.x > 10)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0);
        }
        if (transform.position.y > -1)
        {
            transform.position = new Vector3(transform.position.x, -1, 0);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
    }
}
