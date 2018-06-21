using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovement : CharacterMovement
{
    private Vector2 JumpTimeGap = new Vector2(6f, 20f);
    private Vector2 TurnTimeGap = new Vector2(6f, 20f);
    private Vector2 IdleSpeedXRange = new Vector2(-1f, 1f);
    private float AimTargetDistance = 0.5f;
    public Vector2 MovementZone;
    public Transform Target;

    private bool m_Idling = true;
    private Bounds m_Zone;
    private float m_NextJumpTime = float.MaxValue;
    private float m_NextTurnTime = float.MaxValue;
    private float m_IdleSpeedX = 0f;
    private float m_AimTargetMuti = 1f;
    private Vector3 InitPos;

    private void Start()
    {
        m_Zone = new Bounds(transform.position, MovementZone);
        InitPos = transform.position;
        m_NextJumpTime = Time.time + Random.Range(JumpTimeGap.x, JumpTimeGap.y);
        m_NextTurnTime = Time.time + Random.Range(TurnTimeGap.x, TurnTimeGap.y);
    }

    protected override void Update()
    {
        // Random Jump
        if (Time.time > m_NextJumpTime)
        {
            m_NextJumpTime = Time.time + Random.Range(JumpTimeGap.x, JumpTimeGap.y);
            Jump();
        }
        // Random Turn
        if (Time.time > m_NextTurnTime)
        {
            m_NextTurnTime = Time.time + Random.Range(TurnTimeGap.x, TurnTimeGap.y);
            Turn();
        }
        // Out Zone Turn
        Vector3 pos = transform.position;
        pos.z = m_Zone.center.z;
        if (!m_Zone.Contains(pos))
        {
            m_IdleSpeedX = m_Zone.center.x - transform.position.x;
        }

        base.Update();
    }
    protected override void FixedUpdate()
    {

        Vector3 pos = Target.position;
        pos.z = m_Zone.center.z;
        m_Idling = !m_Zone.Contains(pos);

        Move(Mathf.Clamp( m_Idling ? m_IdleSpeedX : Target.position.x - transform.position.x + AimTargetDistance * m_AimTargetMuti,-1f,1f));

        base.FixedUpdate();
    }
    private void Turn()
    {
        m_IdleSpeedX = Random.Range(IdleSpeedXRange.x, IdleSpeedXRange.y);
        m_AimTargetMuti = Random.value > 0.5f ? 1f : -1f;
    }
    public void BackToInitPos()
    {
        transform.position = InitPos;
    }
}
