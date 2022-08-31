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
    [SerializeField]
    private GameObject _leftDamage;
    [SerializeField]
    private GameObject _rightDamage;
    private bool _shielded = false;
    private int _score = 0;
    private int _shieldPoints = 2;
    [SerializeField]
    private int _maxAmmo = 20;
    private int _ammoLeft;
    private GameObject _thruster;

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _canFire = Time.time;
        _audio = GetComponent<AudioSource>();
        _thruster = transform.Find("Thruster").gameObject;
        _ammoLeft = _maxAmmo;
        _canvas.GetComponent<UIManager>().SetAmmoText(_ammoLeft, _maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire && _ammoLeft > 0)
        {
            _ammoLeft--;
            _canvas.GetComponent<UIManager>().SetAmmoText(_ammoLeft, _maxAmmo);
            _canFire = Time.time + _fireRate;
            if (_tripleShotActive)
                Instantiate(_tripleShot, transform.position + Vector3.up, Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
            _audio.Play();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed *= 2f;
            _thruster.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed /= 2f;
            _thruster.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
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
            _shieldPoints--;
            if (_shieldPoints == 0)
            {
                _shielded = false;
                _playerShield.SetActive(false);
            }
            else
            {
                _playerShield.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            }
        }
        else
        {
            _lives--;
            _canvas.GetComponent<UIManager>().SetLivesImage(_lives);
            if (_lives == 0)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                _canvas.GetComponent<UIManager>().GameOver();
            }
            else if (_lives == 1)
            {
                _rightDamage.SetActive(true);
            }
            else if (_lives == 2)
            {
                _leftDamage.SetActive(true);
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
        if(_shieldPoints < 2)
        {
            _shieldPoints = 2;
            _playerShield.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
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
    public void RefillAmmo()
    {
        _ammoLeft = _maxAmmo;
        _canvas.GetComponent<UIManager>().SetAmmoText(_ammoLeft, _maxAmmo);
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
