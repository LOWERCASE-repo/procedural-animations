#pragma warning disable 0649
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class Mover : MonoBehaviour {
	
	[SerializeField]
	protected float force;
	// should really caps this and force = value * value
	protected float speed {
		get => force;
		set {
			body.drag = speed / body.mass;
			force = value;
		}
	}
	internal Rigidbody2D body;
	
	protected virtual void Awake() {
		body = GetComponent<Rigidbody2D>();
		speed = force;
	}
	
	protected void Move(Vector2 dir) {
		dir = Vector2.ClampMagnitude(dir, 1f);
		Vector2 force = dir * this.force * this.force;
		body.AddForce(force);
	}
}
