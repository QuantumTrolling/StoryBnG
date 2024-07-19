using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cursor : MonoBehaviour
{

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            CheckForUnits();
        }
    }

    private void CheckForUnits(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)){
            Debug.Log("" + hit.collider.gameObject.name);
        }
    }
}
