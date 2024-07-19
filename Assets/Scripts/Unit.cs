using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] public bool isEnemy;
    [SerializeField] public float Max_health;
    [SerializeField] public int speed;
    public Text text_turn;
    public Text text_hp;

    public GameObject Contours;

    private float current_health;
    private int turn;

    private void Start() {
        current_health = Max_health;
    }

    private void Update() {
        text_hp.text = "" + current_health;
    }

    public void TakeDamage(float damage){
        current_health=-damage;
        if (current_health <= 0){
            Death();
        }
    }
    public void SetTurn(int setturn){

        text_turn.text = "" + setturn;
        turn = setturn;
    }

    public void UnitOnClick(){
        Contours.SetActive(true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log("You hit me");
    }


    public void Death(){
        Destroy(gameObject);
    }

}
