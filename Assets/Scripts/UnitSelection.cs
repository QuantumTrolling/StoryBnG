using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection Instance { get; private set; }
    public List<Unit> selectedUnits = new List<Unit>();
    public int MaxUnitsSelected = 3;
    private GameObject Canvas;
    public List<Transform> occupiedDropAreas = new List<Transform>();

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
        Canvas = GameObject.Find("Canvas");
    }

    public void PlaceUnit(GameObject unitPrefab, Transform dropArea, Vector2 coordinates)
    {
        if (unitPrefab == null || dropArea == null)
        {
            Debug.LogError("unitPrefab or dropArea is null");
            return;
        }

        Vector2 realCoordinates = Camera.main.ScreenToWorldPoint(coordinates);
        GameObject newUnit = Instantiate(unitPrefab, Canvas.transform);
        newUnit.transform.position = realCoordinates;

        UnitUI unitUI = newUnit.GetComponent<UnitUI>();
        if (unitUI != null)
        {
            unitUI.OccupiedArea = dropArea;
        }
        else
        {
            Debug.LogError("UnitUI component is missing on the instantiated unit");
        }

        Transform newUnitTransform = newUnit.GetComponent<Transform>();
        if (newUnitTransform != null)
        {
            newUnitTransform.localScale = new Vector3(216, 216, 216);
        }

        Unit newUnitComponent = newUnit.GetComponent<Unit>();
        if (newUnitComponent != null)
        {
            selectedUnits.Add(newUnitComponent);
            UnitsManagement.Instance.AddUnit(newUnitComponent);
        }
        else
        {
            Debug.LogError("Unit component is missing on the instantiated unit");
        }

        if (!occupiedDropAreas.Contains(dropArea))
        {
            occupiedDropAreas.Add(dropArea);
        }
    }

    public void PlaceUnitIcon(GameObject unitIconPrefab)
    {
        if (unitIconPrefab == null)
        {
            Debug.LogError("unitIconPrefab is null");
            return;
        }

        GameObject[] prefabPanels = GameObject.FindGameObjectsWithTag("PanelIcon");

        // Сортируем панели по Y-координате
        System.Array.Sort(prefabPanels, (panel1, panel2) => panel2.transform.position.y.CompareTo(panel1.transform.position.y));

        foreach (GameObject panel in prefabPanels)
        {
            if (panel.transform.childCount == 0)
            {
                GameObject newUnitIcon = Instantiate(unitIconPrefab, panel.transform);
                RectTransform newUnitIconRectTransform = newUnitIcon.GetComponent<RectTransform>();
                if (newUnitIconRectTransform != null)
                {
                    newUnitIconRectTransform.anchoredPosition = Vector2.zero;
                    newUnitIconRectTransform.localScale = Vector3.one;
                    newUnitIconRectTransform.anchorMin = new Vector2(0, 0);
                    newUnitIconRectTransform.anchorMax = new Vector2(0, 0);
                    newUnitIconRectTransform.pivot = new Vector2(0, 0);
                }
                else
                {
                    Debug.LogError("RectTransform component is missing on the instantiated unit icon");
                }
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
        if (unit == null || dropArea == null)
        {
            Debug.LogError("unit or dropArea is null");
            return;
        }

        selectedUnits.Remove(unit);
        UnitsManagement.Instance.RemoveUnit(unit);
        if (occupiedDropAreas.Contains(dropArea))
        {
            occupiedDropAreas.Remove(dropArea);
        }
    }

    public bool IsAllUnitsSelected()
    {
        return selectedUnits.Count == MaxUnitsSelected;
    }
}
