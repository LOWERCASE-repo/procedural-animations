#pragma warning disable 0649
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class Mover : MonoBehaviour {
	
	[SerializeField]
	protected float speed;
	internal Rigidbody2D body;
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}
	
	protected virtual void FixedUpdate() {
		body.drag = speed / body.mass;
	}
	
	protected void Move(Vector2 dir) {
		dir = Vector2.ClampMagnitude(dir, 1f);
		Vector2 force = dir * speed * speed;
		body.AddForce(force);
	}
}