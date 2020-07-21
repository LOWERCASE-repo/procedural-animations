using UnityEngine;
using System.Collections.Generic;

class RopeSeg : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	CircleCollider2D collider;
	
	internal Vector2 Pos {
		get => body.position;
		set => body.position = value;
	}
	internal float Radius { set => collider.radius = value; }
	Vector2 prevPos = Vector2.zero;
	
	internal void VerletUpdate() {
		body.velocity += body.position - prevPos;
		prevPos = body.position;
	}
}
