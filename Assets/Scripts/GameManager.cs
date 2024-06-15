using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsStarted { get; set; }
    [Header("Player")]

    [SerializeField] private Player player;
    [SerializeField] private Vector3 startPosition;


    [Header("UI elements")]
    [SerializeField] private GameObject StartGameUI;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject flashOverlay;
    [SerializeField] private GameObject RestartGameUI;


    [Header("Score")]
    [SerializeField] private ScoreManager scoreManager;


    void Start()
    {
        IsStarted = false;
        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead) return;

        if (IsStarted) return;


        // before start (IsStarted = false)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

    }

    public void StartGame()
    {
        if (player.IsDead) return;

        // Game status
        IsStarted = true;

        // player
        player.IsDead = false;

        player.GetComponent<Rigidbody2D>().gravityScale = 2;
        player.GetComponent<PlayerMovement>().Flap();

        // UI
        StartGameUI.SetActive(false);
        scoreUI.SetActive(true);

    }


    public void GameOver()
    {
        // Game status
        //IsStarted = false;

        // flash
        flashOverlay.GetComponent<Animation>().Play("Flash");

        // save score
        scoreManager.SaveScore();

        // restart UI
        StartCoroutine("ShowRestartScreen");

    }

    IEnumerator ShowRestartScreen()
    {
        yield return new WaitForSeconds(1f);
        RestartGameUI.SetActive(true);
    }

    public void RestartGame()
    {
        // UI

        RestartGameUI.SetActive(false);
        StartGameUI.SetActive(true);
        scoreUI.SetActive(false);

        // Game status
        IsStarted = false;
        scoreManager.ResetScore();
        scoreManager.UpdateScoreUI();


        // Player
        ResetPlayer();

        // Map: remove all Tube;
        GameObject pipeContainer = GameObject.Find("Pipe Container");

        if (pipeContainer)
        {
            Destroy(pipeContainer);
        }

    }

    public void ResetPlayer()
    {
        player.IsDead = false;
        player.GetComponent<Animator>().SetBool("idle", true);
        player.GetComponent<Animator>().ResetTrigger("dead");
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = startPosition;
    }
}
