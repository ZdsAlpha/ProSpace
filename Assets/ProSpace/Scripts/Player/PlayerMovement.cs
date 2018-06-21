using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : CharacterMovement
{
    public float boostMultiplier = 2f;
    public float boost = 100f;
    public float maxBoost = 100f;
    public float boostGeneration = 5f;
    public KeyCode LeftKey = KeyCode.A;
    public KeyCode RightKey = KeyCode.D;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode BoostKey = KeyCode.LeftShift;

    private float prevLTime = -1f;
    private float prevRTime = -1f;

    protected override void Update()
    {
        boost = Mathf.Clamp(boost + boostGeneration * Time.deltaTime, 0f, maxBoost);
        float move = 0f;

        if (Input.GetKey(LeftKey))
        {
            if (prevLTime < 0f)
            {
                prevLTime = Time.time;
            }
            if (prevLTime > prevRTime)
            {
                move = -1f;
            }
        }
        else
        {
            prevLTime = -1f;
        }

        if (Input.GetKey(RightKey))
        {
            if (prevRTime < 0f)
            {
                prevRTime = Time.time;
            }
            if (prevRTime > prevLTime)
            {
                move = 1f;
            }
        }
        else
        {
            prevRTime = -1f;
        }

        if (Input.GetKey(BoostKey) && boost - 5 * Time.deltaTime>=0f)
        {
            Move(move*boostMultiplier);
            boost -= 5 * Time.deltaTime;
        }
        else
        {
            Move(move);
        }

        // Jump
        if (Input.GetKeyDown(JumpKey))
        {
            Jump();
        }
        if (Input.GetKeyUp(JumpKey))
        {
            SwitchGravityScale(false);
        }

        base.Update();
    }

    public void RestoreBoost()
    {
        boost = maxBoost;
    }
}