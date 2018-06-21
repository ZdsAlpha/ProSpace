using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterMovement : MonoBehaviour
{
    private const float GROUND_CHECK_GAP = 0.05f;
    private const float MAX_DROP_SPEED = 24f;

    private CharacterAttackness Attackness
    {
        get
        {
            return m_Attackness ?? (m_Attackness = GetComponent<CharacterAttackness>());
        }
    }
    private Rigidbody2D Rig
    {
        get
        {
            return m_Rig ?? (m_Rig = GetComponent<Rigidbody2D>());
        }
    }
    public bool IsGrounded
    {
        get
        {
            return m_IsGrounded;
        }
        private set
        {
            m_IsGrounded = value;
        }
    }
    public float AimSpeed
    {
        get
        {
            return m_AimSpeed;
        }
        private set
        {
            m_AimSpeed = value;
        }
    }
    public float SpeedY
    {
        get
        {
            return Rig.velocity.y;
        }
    }
    public bool IsMoving
    {
        get
        {
            return Mathf.Abs(AimSpeed) > 0.01f;
        }
    }
    public bool IsJumping
    {
        get
        {
            return !IsGrounded && Mathf.Abs(SpeedY) > 0.1f;
        }
    }

    public float MoveSpeed = 8f;
    public float JumpSpeed = 18f;
    public int JumpCount = 2;
    public float GravityScaleU = 6f;
    public float GravityScaleD = 12f;
    public float GroundCheckWidth = 0.35f;
    public VoidEvent OnJump;
    public BoolEvent Moving;

    private CharacterAttackness m_Attackness;
    private Rigidbody2D m_Rig = null;
    private RaycastHit2D[] GroundCheckHits = new RaycastHit2D[2];
    private float m_AimSpeed = 0f;
    private bool m_IsGrounded = true;
    private int m_CurrentJumpCount = 0;
    private float m_LastJumpTime = float.MinValue;

    protected virtual void Update()
    {
        Moving.Invoke(Mathf.Abs(SpeedY) < 0.1f && Mathf.Abs(AimSpeed) > 0.1f);
    }
    protected virtual void FixedUpdate()
    {

        // Move
        Rig.velocity = new Vector2(
            AimSpeed,
            Mathf.Clamp(Rig.velocity.y, -MAX_DROP_SPEED, MAX_DROP_SPEED)
        );
        // Ground Check
        IsGrounded = GroundCheck(new Vector2(
            transform.position.x - GroundCheckWidth * 0.5f,
            transform.position.y + GROUND_CHECK_GAP
        )) || GroundCheck(new Vector2(
            transform.position.x + GroundCheckWidth * 0.5f,
            transform.position.y + GROUND_CHECK_GAP
        ));
        // Jump Count
        if (IsGrounded && Time.time > m_LastJumpTime + 0.1f)
        {
            m_CurrentJumpCount = 0;
        }
        // Gravity Scale
        if (Rig.gravityScale == GravityScaleU && SpeedY <= 0f)
        {
            SwitchGravityScale(false);
        }

    }
    public void Move(float speedR)
    {
        m_AimSpeed = speedR * MoveSpeed;
        if (speedR != 0f && (Attackness == null || !Attackness.IsAttacking))
        {
            transform.localScale = new Vector3(speedR > 0f ? 1f : -1f, 1f, 1f);
        }
    }
    public void Jump()
    {
        if (m_CurrentJumpCount < JumpCount)
        {
            m_CurrentJumpCount++;
            OnJump.Invoke();
            SwitchGravityScale(true);
            Rig.velocity = new Vector2(Rig.velocity.x, JumpSpeed);
            m_LastJumpTime = Time.time;
        }
    }
    public void SwitchGravityScale(bool isU)
    {
        Rig.gravityScale = isU ? GravityScaleU : GravityScaleD;
    }
    private bool GroundCheck(Vector2 from)
    {
        int len = Physics2D.RaycastNonAlloc(from, Vector2.down, GroundCheckHits, GROUND_CHECK_GAP * 2f);
        return len == 2 || (len == 1 && GroundCheckHits[0].transform != transform);
    }
}
