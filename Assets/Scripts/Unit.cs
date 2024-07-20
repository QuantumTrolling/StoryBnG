using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    [SerializeField] private float maxHealth;
    [SerializeField] private int speed;
    private float currentHealth;
    private int turn;

    public bool IsEnemy { get { return isEnemy; } }
    public float MaxHealth { get { return maxHealth; } }
    public int Speed { get { return speed; } }
    public float CurrentHealth { get { return currentHealth; } }
    public int Turn { get { return turn; } }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void SetTurn(int setTurn)
    {
        turn = setTurn;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

  
    

}
