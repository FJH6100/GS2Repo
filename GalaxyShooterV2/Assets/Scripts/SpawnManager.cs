using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private float _spawnInterval = 5f;
    private bool _alive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        _alive = false;
    }
    IEnumerator SpawnEnemies()
    {
        while(_alive)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 7f, 0f);
            GameObject enemy = Instantiate(_enemy, spawnPosition, Quaternion.identity);
            enemy.transform.parent = this.transform;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
