#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

class Core : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	float speed, thrust;
	
	void Start() {
		body.drag = thrust;
	}
	
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		MoveDir(dir);
	}
	
	void MoveDir(Vector2 dir) {
		Vector2 force = dir.normalized * thrust * speed;
		body.AddForce(force);
	}
}
