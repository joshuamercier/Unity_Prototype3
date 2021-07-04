using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Class variables
    public float jumpForce;
    public float gravityModifier;
    public int maxJumpsAllowed = 2;

    private bool introFinished = false;
    private int currentJumps = 0;
    private bool gameOver = false;
    // Rigibody
    private Rigidbody playerRb;
    // Animator reference
    private Animator playerAnim;
    // Particle effects references
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    // Sound effect references
    public AudioClip jumpSound;
    public AudioClip crashSound;
    // Player adui source
    private AudioSource playerAudio;
    // Score script
    ScoreScript scoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        // Get the animator
        playerAnim = GetComponent<Animator>();
        // Get the audio source
        playerAudio = GetComponent<AudioSource>();
        // Sert start spped_f value
        playerAnim.SetFloat("Speed_f", 0.5f);
        // Get scorescript
        scoreCounter = GameObject.Find("Score").GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -4)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2f);
        }
        else
        {
            introFinished = true;
            playerAnim.SetFloat("Speed_f", 0.6f);
        }

        // Check if dashing
        if (Input.GetKey(KeyCode.LeftShift) && !gameOver)
        {
                CheckJump();
                Time.timeScale = 2.0f;
                scoreCounter.AddScore(10);
        }
        else if(!gameOver)
        {
            CheckJump();
            // Reset time scale
            Time.timeScale = 1.0f;
            scoreCounter.AddScore(1);
        }



    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentJumps != maxJumpsAllowed)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Change animation for jumping
            playerAnim.SetTrigger("Jump_trig");
            // Stop dirt particle from playing
            dirtParticle.Stop();
            // Play jump sound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            // Increment jump count
            currentJumps++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Start dirt particle again when player lands on ground
            dirtParticle.Play();
            // Reset current jumps
            currentJumps = 0;
        } 
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            // Death animation
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            // Play smoke particles
            explosionParticle.Play();
            // Stop dirt particles
            dirtParticle.Stop();
            // Play crash sound
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    // Properties for variables
    public bool GameOver
    {
        get => gameOver;
        set => gameOver = value;
    }

    public bool IntroDone
    {
        get => introFinished;
        set => introFinished = value;
    }
}
