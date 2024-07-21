using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private static UnitUI LastUnitUI;
    public static Unit LastUnit;
    public UnitsManagement unitsManagement;


    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            CheckForUnits();
        }
    }

    private void CheckForUnits(){
        Debug.Log(LastUnit);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent(out UnitUI unitUI)){
            Unit currentUnit = unitsManagement.units[unitsManagement.currentUnitIndex];
            if (LastUnitUI!=null && LastUnitUI!=unitUI){
                currentUnit.GetComponent<UnitUI>().UnitUnClickSkills();
                LastUnitUI.UnitUnClick();
            }
            LastUnitUI = unitUI;
            LastUnitUI.UnitOnClick();
            LastUnit = hit.collider.gameObject.GetComponent<Unit>();
        }
    }
}
