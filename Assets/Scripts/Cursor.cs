using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Unit LastUnit;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            CheckForUnits();
        }
    }

    private void CheckForUnits(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent(out Unit unit)){
            if (LastUnit!=null && LastUnit!=unit){
                LastUnit.UnitUnclick();
            }
            unit.UnitOnClick();
            LastUnit=unit;
        }
    }
}
