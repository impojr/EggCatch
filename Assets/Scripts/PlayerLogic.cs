using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverCanvas;
    public int lives = 3;

    private int points = 0;
    private Animator animator;

    void Awake()
    {
        Time.timeScale = 1; //resets the timescale that is stopped on death
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdatePointsText();
        UpdateLivesText();
    }

    public void AddPoints(int pointsToAdd)
    {
        animator.SetTrigger("CollectedEgg");
        points += pointsToAdd;
        UpdatePointsText();
    }

    public void TakeLife()
    {
        animator.SetTrigger("CollectedBomb");
        lives--;
        UpdateLivesText();
        if (lives <= 0)
        {
            Death();
        }
    }

    public int GetPoints()
    {
        return points;
    }

    private void UpdatePointsText()
    {
        scoreText.text = "Score: " + points.ToString();
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    private void Death()
    {
        Time.timeScale = 0; //stops the items from falling when dead
        gameOverCanvas.SetActive(true);
    }
}
