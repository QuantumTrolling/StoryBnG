using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManagement : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    public Unit CurrentUnit => units[currentUnitIndex];
    public int currentUnitIndex = 0;

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Preparation) { return; }
        if (GameManager.Instance.CurrentState == GameState.Battle) { StartScript(); }
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
        units[currentUnitIndex].SetTurn(units.Count);
        for (int i = 0; i < units.Count; i++)
        {
            if (i != currentUnitIndex)
            {
                units[i].SetTurn(units[i].Turn - 1);
            }
        }
        currentUnitIndex = units.FindIndex(unit => unit.Turn == 1);

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
