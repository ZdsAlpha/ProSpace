namespace MoenenGames.EightBeatMan {
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class StageCamera : MonoBehaviour {



		// Ser
		[SerializeField] private Vector2 m_LerpRate = new Vector2(10f, 1f);
		[SerializeField] private Rigidbody2D m_Player;


		// Cache
		private Vector3 AimPosition;



		private void Awake () {
			AimPosition = transform.position;
		}


		private void LateUpdate () {
			transform.position = AimPosition;
		}


		void FixedUpdate () {
            Vector3 position = m_Player.position;
			AimPosition.x = Mathf.Lerp(
				transform.position.x,
                position.x,
				Time.deltaTime * m_LerpRate.x
			);
			AimPosition.y = Mathf.Lerp(
				transform.position.y,
                position.y,
				Time.deltaTime * m_LerpRate.y
			);
			AimPosition.z = position.z;
		}


	}
}