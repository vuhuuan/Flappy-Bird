using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    AudioSource audioSource;
    public AudioClip deadSound;


    public bool IsDead;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Pipes" || collision.tag == "Ground") && !IsDead)
        {
            IsDead = true;
            Die();
        }
    }

    public void Die()
    {
        audioSource.PlayOneShot(deadSound);
        gameManager.GameOver();
    }
}
