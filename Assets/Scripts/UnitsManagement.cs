using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManagement : MonoBehaviour
{
    public static UnitsManagement Instance { get; private set; }
    public List<Unit> units = new List<Unit>();
    public Unit CurrentUnit => units.Count > 0 && currentUnitIndex >= 0 && currentUnitIndex < units.Count ? units[currentUnitIndex] : null;
    public int currentUnitIndex = 0;

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

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Preparation) { return; }

        if (units.Count == 0 || currentUnitIndex < 0 || currentUnitIndex >= units.Count) { return; }

        if (units[currentUnitIndex].CurrentHealth <= 0)
        {
            NextTurn();
        }
    }

    public void StartScript()
    {
        SortUnitsBySpeed();
        SetTurnOrder();
    }

    private void SortUnitsBySpeed()
    {
        units.Sort((a, b) => b.Speed.CompareTo(a.Speed));
    }

    private void SetTurnOrder()
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].SetTurn(i + 1);
        }
    }

    public void NextTurn()
    {
        if (units.Count == 0) { return; }

        units[currentUnitIndex].SetTurn(units.Count);
        for (int i = 0; i < units.Count; i++)
        {
            if (i != currentUnitIndex)
            {
                units[i].SetTurn(units[i].Turn - 1);
            }
        }
        currentUnitIndex = units.FindIndex(unit => unit.Turn == 1);

        if (currentUnitIndex < 0 || currentUnitIndex >= units.Count) { return; }

        if (units[currentUnitIndex].IsEnemy)
        {
            ExecuteEnemyTurn();
        }
    }

    private void ExecuteEnemyTurn()
    {
        Unit target = GetUnitWithLowestHealth();
        if (target != null)
        {
            units[currentUnitIndex].Skill.Activate(target);
        }
        NextTurn();
    }

    public Unit GetUnitWithLowestHealth()
    {
        Unit lowestHealthUnit = null;
        float lowestHealth = float.MaxValue;

        foreach (Unit unit in units)
        {
            if (!unit.IsEnemy && unit.CurrentHealth < lowestHealth)
            {
                lowestHealth = unit.CurrentHealth;
                lowestHealthUnit = unit;
            }
        }
        return lowestHealthUnit;
    }

    public void AddUnit(Unit unit)
    {
        if (!units.Contains(unit))
        {
            units.Add(unit);
        }
    }

    public void RemoveUnit(Unit unit)
    {
        if (units.Contains(unit))
        {
            units.Remove(unit);
        }
    }
}
