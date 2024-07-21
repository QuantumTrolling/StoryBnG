using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThree : Skill
{
    public float healAmount;
    public override void Activate(Unit target)
    {
        if (!target.IsEnemy) { target.Heal(healAmount); }
        
    }
}
