using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public int points = 0;
    public int lives = 3;

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    public void TakeLife()
    {
        lives--;
        if (lives <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("U DED");
    }
}
