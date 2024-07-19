using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitManipulait : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    private void SortUnits(){
        units.Sort(SortbySpeed);
    }


    private int SortbySpeed(Unit f, Unit s){
        if (f.speed > s.speed){
            return -1;
        }
        else if (f.speed < s.speed){
            return 1;
        }
        return 0;
    }

    private void Start() {
        SortUnits();
        for(int i = 0; i<units.Count;i++){
            units[i].SetTurn(i+1);
        }
    }

}
