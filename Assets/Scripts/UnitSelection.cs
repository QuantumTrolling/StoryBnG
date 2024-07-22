using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static List<Unit> selectedUnits = new List<Unit>();
    [SerializeField] private UnitsManagement unitsManagement;
    public static int MaxUnitsSelected = 3;

    public void PlaceUnit(GameObject unitPrefab, RectTransform dropArea)
    {
        // Удаляем существующую иконку, если она уже перемещена
        foreach (Transform child in dropArea)
        {
            Destroy(child.gameObject);
        }

        // Создаем новый объект
        GameObject newUnit = Instantiate(unitPrefab, dropArea);
        RectTransform newUnitRectTransform = newUnit.GetComponent<RectTransform>();
        if (newUnitRectTransform != null)
        {
            newUnitRectTransform.anchoredPosition = Vector2.zero;
            newUnitRectTransform.localScale = Vector3.one;
        }

        // Добавляем новый объект в список выбранных юнитов и управление юнитами
        Unit newUnitComponent = newUnit.GetComponent<Unit>();
        selectedUnits.Add(newUnitComponent);
        unitsManagement.AddUnit(newUnitComponent);
    }

    public void RemoveUnit(GameObject unit)
    {
        Unit unitComponent = unit.GetComponent<Unit>();
        if (unitComponent != null)
        {
            selectedUnits.Remove(unitComponent);
            unitsManagement.RemoveUnit(unitComponent);
        }
    }

    public static bool IsAllUnitsSelected()
    {
        return selectedUnits.Count == MaxUnitsSelected;
    }
}
