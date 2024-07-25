using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private GameObject UIInterfacePrefab;
    [SerializeField] private Unit unit;
    [SerializeField] private GameObject statusIconPrefab;

    private GameObject UIInterface;
    private TMP_Text textTurn;
    private TMP_Text textHp;
    public GameObject skills;
    private Image healthBar;
    private GameObject selection;
    private Transform statusPanel;
    
    private GameObject IconPrefab;

    // Ссылка на область, занимаемую юнитом
    public Transform OccupiedArea;

    private float maxHealth;
    private Dictionary<string, GameObject> statusIcons = new Dictionary<string, GameObject>();

    private void Start()
    {
        // Инициализация префаба интерфейса
        UIInterface = Instantiate(UIInterfacePrefab, transform.position, Quaternion.identity);
        UIInterface.transform.SetParent(GameObject.Find("Canvas").transform, false);
        // Получение ссылок на дочерние объекты интерфейса
        textTurn = UIInterface.transform.Find("TurnIcon/TurnText").GetComponent<TMP_Text>();
        textHp = UIInterface.transform.Find("HealthBar/HealthText").GetComponent<TMP_Text>();
        skills = UIInterface.transform.Find("Skills").gameObject;
        healthBar = UIInterface.transform.Find("HealthBar").GetComponent<Image>();
        selection = UIInterface.transform.Find("Contours").gameObject;
        statusPanel = UIInterface.transform.Find("StatusPanel");
        maxHealth = unit.MaxHealth;
    }

    private void UpdateChildPositions(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.position = parent.position;
            child.localPosition = Vector3.zero; 
        }
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

        //We connect UI-elements to sprite
        // Translate world position to UI coordinates
        Vector2 pos = this.transform.position;  // get the game object position
        Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

        RectTransform rectTransform = UIInterface.GetComponent<RectTransform>();
        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        rectTransform.anchorMin = viewportPoint;
        rectTransform.anchorMax = viewportPoint;
        rectTransform.position = transform.position + new Vector3(0, 2, 0); 
        //UpdateChildPositions(UIInterface.transform);

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
