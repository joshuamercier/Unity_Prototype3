using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    // Class variables
    private int gameScore = 0;
    private Text scoreText;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameScore;
    }

    public void AddScore(int value)
    {
        // Check that game has not ended and that intro is over
        if (player.GameOver != true && player.IntroDone != false)
        {
            gameScore += value;
            Debug.Log("Score: " + gameScore);
        }
    }

    // Properties for variables
    public int Score
    {
        get => gameScore;
        set => gameScore += value;
    }
}
