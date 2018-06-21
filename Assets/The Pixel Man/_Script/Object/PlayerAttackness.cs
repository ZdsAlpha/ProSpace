namespace MoenenGames.EightBeatMan {
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[DisallowMultipleComponent]
	public class PlayerAttackness : CharacterAttackness {



        public Camera camera;
		// Ser
		[SerializeField] private KeyCode m_Attack = KeyCode.O;



		private void Update () {

			if (Input.GetKey(m_Attack) || Input.GetKey(KeyCode.Mouse0)) {
                Vector3 v = camera.ScreenToWorldPoint(Input.mousePosition);
                v.z = 0;
                v = v - transform.position;
                v.Normalize();
                Attack(v);
			}

		}



	}
}