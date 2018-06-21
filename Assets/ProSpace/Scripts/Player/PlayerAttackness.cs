using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttackness : CharacterAttackness
{
    public Camera camera;
    public KeyCode AttackKey = KeyCode.Mouse0;
    public float fuel = 100f;
    public float maxFuel = 100f;
    public float fuelGeneration = 5f;
    private void Update()
    {
        fuel = Mathf.Clamp(fuel + fuelGeneration * Time.deltaTime, 0f, maxFuel);
        if (Input.GetKey(AttackKey) && fuel - Frequency * Time.deltaTime >= 0f)
        {
            Vector3 v = camera.ScreenToWorldPoint(Input.mousePosition);
            v.z = 0;
            v = v - transform.position;
            v.Normalize();
            Attack(v);
            fuel -= Frequency * Time.deltaTime;
        }

    }
    public void RestoreFuel()
    {
        fuel = maxFuel;
    }
}