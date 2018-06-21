using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public Collider2D Player;
    public Collider2D Region;
    public ParticleSystem System;
    public VoidEvent OnPickUp;
    public float DeathDelay = 5f;
    private bool isPicked = false;
	void Start () {
        if (Region == null) Region = GetComponent<Collider2D>();
        if (System == null) System = GetComponent<ParticleSystem>();
	}
	void Update () {
		if (Physics2D.IsTouching(Player,Region))
        {
            if (!isPicked)
            {
                isPicked = true;
                if (System != null) System.Stop();
                OnPickUp.Invoke();
                Destroy(gameObject, DeathDelay);
            }
        }
	}
}
