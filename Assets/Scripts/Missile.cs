using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Unit target;
    private float damage;
    public float speed = 5f;

    public void Initialize(Unit target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 targetPosition = target.transform.position;
            Vector2 currentPosition = transform.position;
            Vector2 direction = (targetPosition - currentPosition).normalized;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                target = null;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
