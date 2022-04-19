using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
