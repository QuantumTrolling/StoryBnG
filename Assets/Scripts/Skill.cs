using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float cooldown;
    public abstract void Activate(Unit target);
}