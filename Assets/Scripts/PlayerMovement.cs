using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Player player;

    [Header("Sounds")]
    AudioSource audioSource;
    public AudioClip flapSound;



    [Header("Attribute")]
    [SerializeField] private float flapForce = 5f;
    [SerializeField] private float maxHeight = 8f;

    [SerializeField] private GameManager gameManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 4f;

        player = GetComponent<Player>();
    }
    void Update()
    {
        if (player.IsDead)
        {
            anim.SetBool("idle", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);

            anim.SetTrigger("dead");
            return;
        }
        if (!gameManager.IsStarted) return;

        if (gameObject.transform.position.y > maxHeight) return;

        // Phone
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Flap();
            }
        }

        // Computer
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Flap();
        }

        // Animation
        if (rb.velocity.y <= 0)
        {
            anim.SetBool("idle", false);
            anim.SetBool("up", false);
            
            anim.SetBool("down", true);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("idle", false);
            anim.SetBool("down", false);

            anim.SetBool("up", true);
        }
    }

    public void Flap()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
  
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);

        audioSource.PlayOneShot(flapSound);
    }
}
