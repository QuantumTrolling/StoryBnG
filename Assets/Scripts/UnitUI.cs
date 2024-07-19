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
    [SerializeField] private UnitsManagement unitsManagement; 

    private float maxHealth;

    private void Start()
    {
        maxHealth = unit.MaxHealth;
    }

    

    public void UnitOnClick()
    {
        
    }

    public void UnitUnclick()
    {
        
    }

    public void UnitOnSkillButtonClick()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            Unit enemyUnit = enemy.GetComponent<Unit>();
            if (enemyUnit != null)
            {
                enemyUnit.TakeDamage(10); 
            }
        }
        unitsManagement.NextTurn();
    }

    public void Update()
    {
        Debug.Log("Updating UI for unit " + unit.name + " his turn is: " + unit.Turn);

        textHp.text = unit.CurrentHealth.ToString();
        textTurn.text = unit.Turn.ToString();
        healthBar.fillAmount = unit.CurrentHealth / maxHealth;

        if (unit.Turn == 1)
        {
            skills.SetActive(true);
        }
        else
        {
            skills.SetActive(false);
        }

    }



}
