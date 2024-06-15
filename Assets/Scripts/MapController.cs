using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Window")]
    private int currentWindow = 0;

    [SerializeField] private GameObject[] MovingWindows;

    [SerializeField] private float movingSpeed = 0.04f;

    [Header("Pipe")]
    [SerializeField] private GameObject PipePrefab;

    [SerializeField] private Vector3 spawnPosition;

    [SerializeField] private float distance = 1f;
    [SerializeField] private float pipeMinHeight = -1.5f;
    [SerializeField] private float pipeMaxHeight = 2.5f;
    [SerializeField] private float minDifferent = 0.4f;

    [Header("Player")]
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;



    private void Start()
    {
        InvokeRepeating("RandomPipes", 0f, distance);
    }

    private void Update()
    {
        if (MovingWindows[currentWindow].transform.position.x <= 0)
        {
            RepeatMap();
        }

    }

    private void FixedUpdate()
    {
        if (player.IsDead) return;
        MoveLeft();
    }
    public void MoveLeft()
    {
        MovingWindows[currentWindow].transform.position += Vector3.left * movingSpeed;
        MovingWindows[1 - currentWindow].transform.position += Vector3.left * movingSpeed;
    }

    //Repeat Function
    void RepeatMap()
    { 
        MovingWindows[1 - currentWindow].transform.position = new Vector3(MovingWindows[currentWindow].GetComponent<BoxCollider2D>().size.x - 0.04f,
            MovingWindows[1 - currentWindow].transform.position.y, MovingWindows[1 - currentWindow].transform.position.z);
        currentWindow = 1 - currentWindow;
    }


    float lastRandomHeight = 10f;

    void RandomPipes()
    {
        if (gameManager.IsStarted && !player.IsDead)
        {
            float randomHeight = UnityEngine.Random.Range(pipeMinHeight, pipeMaxHeight);
            while (Mathf.Abs(randomHeight - lastRandomHeight) < minDifferent)
            {
                randomHeight = UnityEngine.Random.Range(pipeMinHeight, pipeMaxHeight);
            }

            GameObject pipeContainer = GameObject.Find("Pipe Container");

            if (!pipeContainer)
            {
                pipeContainer = new GameObject("Pipe Container");
            }

            GameObject pipe = Instantiate(PipePrefab, new Vector3(spawnPosition.x, randomHeight, spawnPosition.z), Quaternion.identity);

            pipe.transform.SetParent(pipeContainer.transform);
   
            lastRandomHeight = randomHeight;
        }
    }

    public void StopSpawnPipes()
    {
        CancelInvoke("RandomPipes");
    }

    public void CleanPipes()
    {
        GameObject pipeContainer = GameObject.Find("Pipe Container");
        if (pipeContainer) Destroy(pipeContainer);
    }
}
