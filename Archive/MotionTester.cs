#pragma warning disable 0649
using UnityEngine;

class MotionTester : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	float speed;
	
	void Start() {
		body.drag = speed / body.mass;
	}
	
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 force = dir.normalized * speed * speed;
		body.AddForce(force);
	}
}
