using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOne : Skill
{
    public GameObject missilePrefab;
    public float damage;

    public override void Activate(Unit target)
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        missile.GetComponent<Missile>().Initialize(target, damage);
    }
}
