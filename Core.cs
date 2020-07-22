#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class Core : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	CircleCollider2D circle;
	[SerializeField]
	float speed, thrust;
	[SerializeField]
	Rope[] ropeRefs;
	HashSet<Rope> ropes;
	HashSet<Rigidbody2D> targets = new HashSet<Rigidbody2D>();
	Dictionary<Rigidbody2D, Rope> grabs = new Dictionary<Rigidbody2D, Rope>();
	int connections;
	
	void Start() {
		body.drag = thrust;
		ropes = new HashSet<Rope>(ropeRefs);
		ropeRefs = null;
		AssignGrabs();
	}
	
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		float speed = 0f;
		foreach (Rope rope in grabs.Values) {
			if (rope.body.bodyType == RigidbodyType2D.Static) {
				speed += this.speed;
			}
		}
		Vector2 force = dir.normalized * thrust * speed;
		body.AddForce(force);
		circle.offset = Vector2.ClampMagnitude(body.velocity / this.speed, 1f) * circle.radius * 0.5f;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		targets.Add(other.attachedRigidbody);
		AssignGrabs();
	}
	
	void OnTriggerExit2D(Collider2D other) {
		Rigidbody2D target = other.attachedRigidbody;
		if (targets.Contains(target)) {
			targets.Remove(target);
		} else {
			Rope rope = grabs[target];
			rope.Release();
			ropes.Add(rope);
			grabs.Remove(target);
			AssignGrabs();
		}
	}
	
	void AssignGrabs() {
		while (ropes.Count > 0 && targets.Count > 0) {
			Rope rope = ropes.ElementAt(0);
			Rigidbody2D target = targets.ElementAt(0);
			rope.target = target;
			grabs.Add(target, rope);
			ropes.Remove(rope);
			targets.Remove(target);
		}
		foreach (Rope rope in ropes) {
			rope.target = body;
		}
	}
}
