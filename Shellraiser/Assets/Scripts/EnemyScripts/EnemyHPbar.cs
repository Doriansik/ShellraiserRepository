using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Slider sliderEnemy;

    private void Start()
    {
        sliderEnemy.interactable = false;
    }

    public void SetMaxHealth(int health)
    {
        sliderEnemy.maxValue = health;
        sliderEnemy.value = health;
    }

    public void SetHealth(float health)
    {
        sliderEnemy.value = health;
    }
}