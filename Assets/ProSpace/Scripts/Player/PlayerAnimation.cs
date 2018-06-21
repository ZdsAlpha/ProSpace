using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent, RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator Animator
    {
        get
        {
            return m_Ani ?? (m_Ani = GetComponent<Animator>());
        }
    }
    private PlayerMovement Movement
    {
        get
        {
            return m_Movement ?? (m_Movement = GetComponentInParent<PlayerMovement>());
        }
    }
    private PlayerAttackness Attackness
    {
        get
        {
            return m_Attackness ?? (m_Attackness = GetComponentInParent<PlayerAttackness>());
        }
    }
    public Transform m_DiedParticlePrefab;
    private string MovingKey = "Moving";
    private string JumpingKey = "Jumping";
    private string SpeedYKey = "SpeedY";
    private string AttackingKey = "AttackID";

    private Animator m_Ani = null;
    private PlayerMovement m_Movement = null;
    private PlayerAttackness m_Attackness = null;

    private void Update()
    {
        Animator.SetBool(MovingKey, Mathf.Abs(Movement.AimSpeed) > 0.01f);
        Animator.SetBool(JumpingKey, !Movement.IsGrounded && Mathf.Abs(Movement.SpeedY) > 0.1f);
        Animator.SetFloat(SpeedYKey, Movement.SpeedY);
        Animator.SetFloat(AttackingKey, Time.time > Attackness.NextAttackTime + 0.1f ? 0f : 1f);
    }
    public void Kill()
    {
        if (m_DiedParticlePrefab)
        {
            var tf = Instantiate(m_DiedParticlePrefab, transform.position, Quaternion.identity);
            Destroy(tf.gameObject, 2f);
        }
        if(transform.parent.GetComponent<Health>().Lives==0) transform.parent.gameObject.TrySetActive(false);
    }
}