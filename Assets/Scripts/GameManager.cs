using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum GameState
{
    Preparation,
    Battle
}

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CurrentState = GameState.Preparation;
    }

    public void StartBattle()
    {
        if (UnitSelection.IsAllUnitsSelected())
        {
            CurrentState = GameState.Battle;
        }
        else
        {
            Debug.LogError("Not all units are placed");
        }
    }

   
    
}
