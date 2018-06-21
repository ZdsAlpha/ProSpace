using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
public class EnemyAttackness : CharacterAttackness
{
    public Collider2D TargetCollider;
    public Collider2D AttackRange;

    private void FixedUpdate()
    {
        if (Physics2D.IsTouching(AttackRange, TargetCollider))
        {
            Attack();
        }
    }
}
