using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool _gameOver = false;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetLivesImage(int lives)
    {
        _livesImage.sprite = _livesSprites[lives];
    }

    public void SetText(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        _gameOver = true;
        StartCoroutine(GameOverFlicker());
        _restartText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.enabled = true;
            yield return new WaitForSeconds(.1f);
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(.1f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_gameOver && Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}
