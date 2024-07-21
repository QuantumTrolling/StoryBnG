using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTwo : Skill
{
    public float damageOverTime;
    public float duration;

    public override void Activate(Unit target)
    {
        DamageOverTime dot = target.gameObject.AddComponent<DamageOverTime>();
        dot.Initialize(damageOverTime, duration, target);
    }
}
