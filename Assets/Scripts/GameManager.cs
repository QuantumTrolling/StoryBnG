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

    [SerializeField] private UIManager uiManager;
    
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    [SerializeField] private GameObject[] enemies;

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
        foreach (var enemy in enemies)
        {
            Instantiate(enemy, enemy.transform.position, Quaternion.identity);
        }
    }

    public void StartBattle()
    {
        if (UnitSelection.Instance.IsAllUnitsSelected())
        {
            CurrentState = GameState.Battle;
            uiManager.DestroyPreparationUI();
            UnitsManagement.Instance.StartScript();
        }
        else
        {
            Debug.LogError("Not all units are placed");
        }
    }

   
    
}
