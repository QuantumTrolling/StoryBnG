using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private TMP_Text textTurn;
    [SerializeField] private TMP_Text textHp;
    [SerializeField] private GameObject skills;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject selection;
    [SerializeField] private Transform statusPanel;
    [SerializeField] private GameObject statusIconPrefab;
    [SerializeField] private GameObject IconPrefab;

    //объекты до входа в бой
    public Transform OccupiedArea;

    private float maxHealth;
    private Dictionary<string, GameObject> statusIcons = new Dictionary<string, GameObject>();

    private void Start()
    {
        maxHealth = unit.MaxHealth;
    }

    public void UnitOnClickSkills()
    {
        if (!unit.IsEnemy)
        {
            skills.SetActive(true);
        }
    }

    public void UnitOnClick()
    {
        selection.SetActive(true);
    }

    public void UnitUnClick()
    {
        selection.SetActive(false);
    }

    public void UnitUnClickSkills()
    {
        selection.SetActive(false);
        if (!unit.IsEnemy)
        {
            skills.SetActive(false);
        }
    }

    public void UnitOnSkillButtonClick()
    {
        if (UnitsManagement.Instance.CurrentUnit == unit)
        {
            Unit target = Cursor.LastUnit;
            if (target != null)
            {
                if (unit.Skill != null)
                {
                    unit.Skill.Activate(target);
                    UnitsManagement.Instance.NextTurn();
                    UnitUnClickSkills();
                }
                else
                {
                    Debug.LogError("Skill is null for unit: " + unit.name);
                }
            }
            else
            {
                Debug.LogError("Target unit is null");
            }
        }
        else
        {
            Debug.LogError("Current unit is not the same as this unit");
        }
    }

    private void UpdateStatusIcons()
    {
        foreach (string status in unit.Statuses)
        {
            if (!statusIcons.ContainsKey(status))
            {
                GameObject icon = Instantiate(statusIconPrefab, statusPanel);
                statusIcons[status] = icon;
            }
        }

        List<string> statusesToRemove = new List<string>();
        foreach (string status in statusIcons.Keys)
        {
            if (!unit.Statuses.Contains(status))
            {
                Destroy(statusIcons[status]);
                statusesToRemove.Add(status);
            }
        }

        foreach (string status in statusesToRemove)
        {
            statusIcons.Remove(status);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.CurrentState == GameState.Preparation)
        {
            UnitSelection.Instance.RemoveUnit(unit, OccupiedArea);
            UnitSelection.Instance.PlaceUnitIcon(IconPrefab);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }

    public void Update()
    {
        if (unit.Turn == 1)
        {
            UnitOnClickSkills();
        }
        UpdateStatusIcons();
        int displayHealth = (int)System.Math.Round(unit.CurrentHealth);
        textHp.text = displayHealth.ToString();
        textTurn.text = unit.Turn.ToString();
        healthBar.fillAmount = unit.CurrentHealth / maxHealth;
    }
}
