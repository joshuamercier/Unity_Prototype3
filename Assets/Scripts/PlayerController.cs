using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Class variables
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver = false;
    public int maxJumpsAllowed = 2;

    private int currentJumps = 0;
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
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        // Get the animator
        playerAnim = GetComponent<Animator>();
        // Get the audio source
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && currentJumps != maxJumpsAllowed)
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
}
