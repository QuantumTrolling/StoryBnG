using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool isEnemy;
    [SerializeField] private float maxHealth;
    [SerializeField] private int speed;
    [SerializeField] private Skill skill;

    private Camera mainCamera;
    private List<string> statuses = new List<string>();
    private float currentHealth;
    private int turn;

    public bool IsEnemy { get { return isEnemy; } }
    public float MaxHealth { get { return maxHealth; } }
    public int Speed { get { return speed; } }
    public float CurrentHealth { get { return currentHealth; } }
    public int Turn { get { return turn; } }
    public Skill Skill { get { return skill; } }
    public List<string> Statuses { get { return statuses; } }

    private void Start()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;
    }

    private void Update()
    {
       
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void SetTurn(int setTurn)
    {
        turn = setTurn;
    }

    public void AddStatus(string status)
    {
        if (!statuses.Contains(status))
        {
            statuses.Add(status);
        }
    }

    public void RemoveStatus(string status)
    {
        if (statuses.Contains(status))
        {
            statuses.Remove(status);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

  
    

}
