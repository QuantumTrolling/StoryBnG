using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private TMP_Text textTurn;
    [SerializeField] private TMP_Text textHp;
    [SerializeField] private GameObject skills;
    [SerializeField] private Image healthBar;
    [SerializeField] private UnitsManagement unitsManagement;
    [SerializeField] private GameObject selection;
    [SerializeField] private Transform statusPanel;
    [SerializeField] private GameObject statusIconPrefab;

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

    public void UnitUn—lickSkills()
    {
        selection.SetActive(false);
        if (!unit.IsEnemy)
        {
            skills.SetActive(false);
        }
    }

    public void UnitOnSkillButtonClick()
    {
        if (unitsManagement.CurrentUnit == unit)
        {
            Unit target = Cursor.LastUnit;
            if (target != null)
            {
                unit.Skill.Activate(target);
            }
            unitsManagement.NextTurn();
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


    public void Update()
    {
        UpdateStatusIcons();
        int displayHealth = (int)System.Math.Round(unit.CurrentHealth);
        textHp.text = displayHealth.ToString();
        textTurn.text = unit.Turn.ToString();
        healthBar.fillAmount = unit.CurrentHealth / maxHealth;

        
    }



}
