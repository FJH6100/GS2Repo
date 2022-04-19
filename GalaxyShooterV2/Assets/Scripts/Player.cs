using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .1f;
    private int _lives = 3;
    private bool _isPowerupActive = false;
    private float _canFire;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private GameObject _tripleShot;
    private bool _tripleShotActive = false;
    [SerializeField]
    private GameObject _playerShield;
    private bool _shielded = false;
    private int _score = 0;

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

    public void IncreaseScore()
    {
        _score += 10;
        _canvas.GetComponent<UIManager>().SetText(_score);
    }

    public void Damage()
    {
        if (_shielded)
        {
            _shielded = false;
            _playerShield.SetActive(false);
        }
        else
        {
            _lives--;
            _canvas.GetComponent<UIManager>().SetLivesImage(_lives);
            if (_lives == 0)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }

    public void ActivateShield()
    {
        if (!_shielded)
        {
            _playerShield.SetActive(true);
            _shielded = true;
        }
    }

    public void ActivateTripleShot()
    {
        StartCoroutine(TripleShotActive());
    }

    IEnumerator TripleShotActive()
    {
        if (!_isPowerupActive)
        {
            _tripleShotActive = true;
            yield return new WaitForSeconds(5f);
            _tripleShotActive = false;
        }
    }

    public void ActivateSpeed()
    {
        StartCoroutine(SpeedActive());
    }

    IEnumerator SpeedActive()
    {
        if (!_isPowerupActive)
        {
            _speed *= 2f;
            yield return new WaitForSeconds(5f);
            _speed /= 2f;
        }
    }

    void PlayerMovement()
    {
        float horizonal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizonal, vertical, 0);
        transform.Translate(direction * Time.deltaTime * _speed);
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
