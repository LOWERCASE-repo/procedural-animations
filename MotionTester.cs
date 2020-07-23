#pragma warning disable 0649
using UnityEngine;

class MotionTester : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	float speed;
	[SerializeField]
	float jumpHeight, bestHeight, jumpForce;
	
	void Start() {
		body.drag = speed / body.mass;
		// jumpForce = Mathf.Sqrt(2f * Physics2D.gravity.magnitude * jumpHeight * body.mass);
		// jumpForce = 2f * Physics2D.gravity.magnitude * jumpHeight * body.mass * body.drag;
		
		/*
		gravity = 10
		height = 2
		mass = 4
		drag = 2.5
		velocity = ~10.2
		
		dragForceMagnitude = velocity.magnitude * drag; // The variable you're talking about
		dragForceVector = dragForceMagnitude * -velocity.normalized;
		
		vel = vel.add(acc);
    vel = vel.mult(model.drag);
		
		velocity = velocity * ( 1 - deltaTime * drag);
		*/
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) body.velocity = new Vector2(0f, jumpForce);
	}
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
		Vector2 force = dir.normalized * speed * speed;
		body.AddForce(force);
		if (body.position.y > bestHeight) bestHeight = body.position.y;
		// Debug.Log(body.velocity.magnitude + " " + body.position.y);
	}
}
