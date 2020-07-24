#pragma warning disable 0649
using UnityEngine;

class MotionTester : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	float speed, jumpForce;
	// float jumpHeight;
	
	void Start() {
		body.drag = speed / body.mass;
		// jumpForce = Mathf.Sqrt(2f * Physics2D.gravity.magnitude * jumpHeight);
		
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			body.velocity = new Vector2(body.velocity.x, jumpForce);
		}
	}
	
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
		if (dir.x == 0) { dir = new Vector2(-body.velocity.x, dir.y); };
		Vector2 force = dir.normalized * speed * speed;
		body.AddForce(force);
		Debug.Log(body.velocity.magnitude);
	}
}
