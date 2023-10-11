using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public int health = 100;

    public void NoteHit()
    {
        health += 10;
        UpdateHealthBar();
    }

    public void NoteMissed()
    {
        health -= 10;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = health;
    }
}