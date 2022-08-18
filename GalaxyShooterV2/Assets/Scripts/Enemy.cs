using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4f;

    private Player _player;

    private Animator _anim;

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
                _player.IncreaseScore();
            _enemySpeed = 0f;
            GetComponent<Animator>().SetTrigger("OnEnemyDeath");
            GetComponent<BoxCollider2D>().enabled = false;
            _audio.Play();
            Destroy(this.gameObject, 2.8f);
        }
        if (other.gameObject.tag == "Player")
        {
            if (_player != null)
                _player.Damage();
            _enemySpeed = 0f;
            GetComponent<Animator>().SetTrigger("OnEnemyDeath");
            GetComponent<BoxCollider2D>().enabled = false;
            _audio.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
}
