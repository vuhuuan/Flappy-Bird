using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI scoreBoard;
    [SerializeField] private TextMeshProUGUI bestScoreBoard;

    AudioSource audioSource;
    public AudioClip pointSound;


    private int score = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", 0);
        }
        bestScoreBoard.text = PlayerPrefs.GetInt("Best Score").ToString();
    }

    void Update()
    {
       
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreUI();

        audioSource.PlayOneShot(pointSound);
    }

    public void DecreaseScore()
    {
        score--;
    }

    public void ResetScore()
    {
        score = 0;
        //UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreUI.text = score.ToString();
    }

    public void SaveScore()
    {
        scoreBoard.text = score.ToString();
        
        if (score > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", score);
            bestScoreBoard.text = score.ToString();
        }
    }

}
