using UnityEngine;

class Motor : MonoBehaviour {
	
	[SerializeField]
	float speed = 10f;
	[SerializeField]
	float thrust = 10f;
	[SerializeField]
	Rigidbody2D rb;
	
	internal void Move(Vector2 target) {
		// target = Predict(target, -rb.velocity, speed);
		MoveDir(target - rb.position);
	}
	
	internal void MoveDir(Vector2 dir) {
		rb.drag = thrust;
		Vector2 force = dir.normalized * thrust * speed;
		rb.AddForce(force);
	}
}
