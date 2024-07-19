using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManagement : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private int currentUnitIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SortUnitsBySpeed();
        SetTurnOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (units[currentUnitIndex].CurrentHealth <= 0)
        {
            NextTurn();
        }
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
    }


}
