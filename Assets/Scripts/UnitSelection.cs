using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static List<Unit> selectedUnits = new List<Unit>();
    [SerializeField] private UnitsManagement unitsManagement;
    public static int MaxUnitsSelected = 3;
    private GameObject Canvas;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void PlaceUnit(GameObject unitPrefab, Transform dropArea, Vector2 coordinates)
    {
        Vector2 realCoordinates = Camera.main.ScreenToWorldPoint(coordinates);
        GameObject newUnit = Instantiate(unitPrefab, Canvas.transform);
        newUnit.transform.position = realCoordinates;
        Transform newUnitTransform = newUnit.GetComponent<Transform>();
        if (newUnitTransform != null)
        {
            newUnitTransform.localScale = new Vector3(216, 216, 216);
        }
        Unit newUnitComponent = newUnit.GetComponent<Unit>();
        selectedUnits.Add(newUnitComponent);
        unitsManagement.AddUnit(newUnitComponent);
    }

    public void PlaceUnitIcon(GameObject unitIconPrefab)
    {
        GameObject[] prefabPanels = GameObject.FindGameObjectsWithTag("PanelIcon");

        foreach (GameObject panel in prefabPanels)
        {
            if (panel.transform.childCount == 0)
            {
                GameObject newUnitIcon = Instantiate(unitIconPrefab, panel.transform);
                RectTransform newUnitIconRectTransform = newUnitIcon.GetComponent<RectTransform>();
                
                    newUnitIconRectTransform.anchoredPosition = Vector2.zero;
                    newUnitIconRectTransform.localScale = Vector3.one;
                
                return;
            }
        }
    }

    public bool IsSlotFree(Transform dropArea)
    {
        return dropArea.childCount == 0;
    }

    public void RemoveUnit(Unit unit)
    {
            selectedUnits.Remove(unit);
            unitsManagement.RemoveUnit(unit);
    }

    public static bool IsAllUnitsSelected()
    {
        return selectedUnits.Count == MaxUnitsSelected;
    }
}
