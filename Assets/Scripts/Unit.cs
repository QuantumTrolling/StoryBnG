using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public bool isEnemy;
    [SerializeField] public float Max_health;
    [SerializeField] public int speed;

    private float current_health;

    public void TakeDamage(float damage){
        current_health=-damage;
        Debug.Log(gameObject.name + " current health is " + current_health);
        if (current_health <= 0){
            Death();
        }
    }

    public void Death(){
        Destroy(gameObject);
    }

}
