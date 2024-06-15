using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 0.04f;
    [SerializeField] private float outtaWindowPosition = -10f;
    private float playerPosition = -1f;
    [SerializeField] private Player player;

    bool point = false;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerPosition = player.transform.position.x + 0.25f;
    }
    private void Update()
    {
        if (!point && transform.position.x <= playerPosition)
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().IncreaseScore();
            point = true;
        }

        if (transform.position.x < outtaWindowPosition)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (! player.IsDead)
        {
            transform.position += Vector3.left * movingSpeed;
        }
    }
}
