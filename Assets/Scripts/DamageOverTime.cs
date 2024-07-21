using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private float damagePerSecond;
    private float duration;
    private Unit target;

    public void Initialize(float damagePerSecond, float duration, Unit target)
    {
        this.damagePerSecond = damagePerSecond;
        this.duration = duration;
        this.target = target;
        target.AddStatus("dot");
        StartCoroutine(ApplyDamageOverTime());
    }

    private IEnumerator ApplyDamageOverTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.TakeDamage(damagePerSecond * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.RemoveStatus("dot");
        Destroy(this);
    }
}
