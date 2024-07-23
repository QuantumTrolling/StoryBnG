using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static List<Unit> selectedUnits = new List<Unit>();
    [SerializeField] private UnitsManagement unitsManagement;
    public static int MaxUnitsSelected = 3;
    private GameObject Canvas;
    private List<Transform> occupiedDropAreas = new List<Transform>();

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void PlaceUnit(GameObject unitPrefab, Transform dropArea, Vector2 coordinates)
    {
        Vector2 realCoordinates = Camera.main.ScreenToWorldPoint(coordinates);
        GameObject newUnit = Instantiate(unitPrefab, Canvas.transform);
        newUnit.transform.position = realCoordinates;
        newUnit.GetComponent<UnitUI>().OccupiedArea = dropArea;
        Transform newUnitTransform = newUnit.GetComponent<Transform>();
        if (newUnitTransform != null)
        {
            newUnitTransform.localScale = new Vector3(216, 216, 216);
        }
        Unit newUnitComponent = newUnit.GetComponent<Unit>();
        selectedUnits.Add(newUnitComponent);
        unitsManagement.AddUnit(newUnitComponent);
        if (!occupiedDropAreas.Contains(dropArea))
        {
            occupiedDropAreas.Add(dropArea);
        }
    }

    public void PlaceUnitIcon(GameObject unitIconPrefab)
    {
        GameObject[] prefabPanels = GameObject.FindGameObjectsWithTag("PanelIcon");

        // ��������� ������ �� Y-����������
        System.Array.Sort(prefabPanels, (panel1, panel2) => panel2.transform.position.y.CompareTo(panel1.transform.position.y));

        foreach (GameObject panel in prefabPanels)
        {
            if (panel.transform.childCount == 0)
            {
                GameObject newUnitIcon = Instantiate(unitIconPrefab, panel.transform);
                RectTransform newUnitIconRectTransform = newUnitIcon.GetComponent<RectTransform>();
                newUnitIconRectTransform.anchoredPosition = Vector2.zero;
                newUnitIconRectTransform.localScale = Vector3.one;
                newUnitIconRectTransform.anchorMin = new Vector2(0, 0);
                newUnitIconRectTransform.anchorMax = new Vector2(0, 0);
                newUnitIconRectTransform.pivot = new Vector2(0, 0);
                return;
            }
        }
    }

    public bool IsSlotFree(Transform dropArea)
    {
        return !occupiedDropAreas.Contains(dropArea);
    }

    public void RemoveUnit(Unit unit, Transform dropArea)
    {
        selectedUnits.Remove(unit);
        unitsManagement.RemoveUnit(unit);
        if (occupiedDropAreas.Contains(dropArea))
        {
            occupiedDropAreas.Remove(dropArea);
        }
    }

    public static bool IsAllUnitsSelected()
    {
        return selectedUnits.Count == MaxUnitsSelected;
    }
}
