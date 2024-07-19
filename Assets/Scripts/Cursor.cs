using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private UnitUI LastUnitUI;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            CheckForUnits();
        }
    }

    private void CheckForUnits(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent(out UnitUI unit)){
            if (LastUnitUI!=null && LastUnitUI!=unit){
                LastUnitUI.UnitUnclick();
            }
            unit.UnitOnClick();
            LastUnitUI=unit;
        }
    }
}
