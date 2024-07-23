using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject unitPrefab;
    public List<RectTransform> dropAreas;
    public UnitSelection unitSelection;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameObject[] dropAreaObjects = GameObject.FindGameObjectsWithTag("DropArea");
        foreach (GameObject dropAreaObject in dropAreaObjects)
        {
            if (dropAreaObject.TryGetComponent<RectTransform>(out RectTransform dropAreaRectTransform))
            {
                dropAreas.Add(dropAreaRectTransform);
            }
            else
            {
                Debug.LogWarning("Object with tag 'dropArea' does not have a RectTransform component: " + dropAreaObject.name);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentState == GameState.Battle)
        {
            return;
        }
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.localScale.x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.CurrentState == GameState.Battle)
        {
            return;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        foreach (RectTransform dropArea in dropAreas)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(dropArea, Input.mousePosition, Camera.main))
            {
                unitSelection.PlaceUnit(unitPrefab, dropArea, eventData.position);
                if (this.gameObject.name.Contains("Icon")) {
                    Destroy(this.gameObject);
                }
                return;
            }
        }
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
