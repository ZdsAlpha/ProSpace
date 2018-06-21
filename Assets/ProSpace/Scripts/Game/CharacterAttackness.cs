using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class CharacterAttackness : MonoBehaviour
{
    private float RandomAttack
    {
        get
        {
            return Random.Range(AttackMin, AttackMax);
        }
    }
    public float NextAttackTime
    {
        get
        {
            return m_NextAttackTime;
        }
        private set
        {
            m_NextAttackTime = value;
        }
    }
    public float PrevAttackTime
    {
        get
        {
            return m_PrevAttackTime;
        }
        private set
        {
            m_PrevAttackTime = value;
        }
    }
    public bool IsAttacking
    {
        get
        {
            return Time.time > NextAttackTime + 0.1f ? false : true;
        }
    }
    public float AttackMin = 5f;
    public float AttackMax = 10f;
    public float Frequency = 10f;
    public float MaxDistance = 15f;
    public float AttackHeight = 1f;
    public float TrailDuration = 0.15f;
    public float ExplosionDuration = 0.2f;
    public bool HealthTargetOnly = false;
    public string Team = "";
    public Transform Pivot;
    public Transform Explosion;
    public LineRenderer BulletTrail;
    public VoidEvent OnAttack;

    private float m_PrevAttackTime = float.MinValue;
    private float m_NextAttackTime = float.MinValue;


    public void Attack()
    {
        Attack(Vector2.zero);
    }
    public void Attack(Vector2 direction)
    {
        if (Time.time > NextAttackTime)
        {
            NextAttackTime = Time.time + 1f / Frequency;
            PrevAttackTime = Time.time;
            if (direction == Vector2.zero)
            {
                direction = transform.localScale.x > 0f ? Vector2.right : Vector2.left;
            }
            transform.localScale = new Vector3(direction.x >= 0f ? 1f : -1f, 1f,1f);
            // Msg
            OnAttack.Invoke();
            // Ray
            RaycastHit2D[] hits = Physics2D.BoxCastAll(
                Pivot.position,
                new Vector2(0.1f, AttackHeight),
                0f,
                direction,
                MaxDistance
            );

            bool hited = false;
            Vector2 point = (Vector2)transform.position + direction.normalized * MaxDistance;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform != transform)
                {

                    bool isTrigger = hits[i].collider.isTrigger;

                    // Max Dis Target
                    if (!isTrigger)
                    {
                        point = hits[i].point;
                        hited = true;
                    }

                    // Health
                    Health health = hits[i].collider.transform.GetComponent<Health>();
                    if (health && (string.IsNullOrEmpty(Team) || string.IsNullOrEmpty(health.Team) || Team != health.Team))
                    {
                        health.TakeDamage(isTrigger ? 0f : RandomAttack);
                        if (!isTrigger)
                        {
                            break;
                        }
                    }

                    if (!HealthTargetOnly && !health && !isTrigger)
                    {
                        break;
                    }

                }
            }

            // Explosion
            if (hited)
            {
                if (Explosion)
                {
                    var tf = Instantiate(
                        Explosion,
                        new Vector3(point.x, point.y , 0f) + Vector3.back * 0.1f,
                        Quaternion.identity
                    );
                    Destroy(tf.gameObject, ExplosionDuration);
                }
            }
            // Line Renderer
            if (BulletTrail)
            {
                StartCoroutine(SpawnBulletTrail(Pivot.position, point ));
            }

        }
    }
    IEnumerator SpawnBulletTrail(Vector3 pivotPos, Vector2 point)
    {
        var lr = Instantiate(BulletTrail, Vector3.zero, Quaternion.identity);
        lr.SetPosition(0, pivotPos);
        lr.SetPosition(1, new Vector3(point.x, point.y, 0f));
        Destroy(lr.gameObject, TrailDuration + 0.5f);
        Color cStart = lr.startColor;
        Color cEnd = lr.endColor;
        var yi = new WaitForEndOfFrame();
        for (float t = 0f; t < TrailDuration; t += Time.deltaTime)
        {
            lr.startColor = Color.Lerp(cStart, Color.clear, t / TrailDuration);
            lr.endColor = Color.Lerp(cEnd, Color.clear, t / TrailDuration);
            yield return yi;
        }
    }
}