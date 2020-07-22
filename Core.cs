#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class Core : MonoBehaviour {
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	float speed, thrust, speedBonus;
	[SerializeField]
	Rope[] ropeRefs;
	HashSet<Rope> ropes;
	HashSet<Vector2> targets = new HashSet<Vector2>();
	Dictionary<Vector2, Rope> grabs = new Dictionary<Vector2, Rope>();
	int connections;
	
	void Start() {
		body.drag = thrust;
		ropes = new HashSet<Rope>(ropeRefs);
		ropeRefs = null;
	}
	
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		float bonusSpeed = 0f;
		foreach (Rope rope in ropes) {
			if (rope.attached) bonusSpeed += speedBonus;
		}
		Vector2 force = dir.normalized * thrust * (speed + speedBonus);
		body.AddForce(force);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		targets.Add(other.transform.position);
		AssignGrabs();
	}
	
	void OnTriggerExit2D(Collider2D other) {
		Vector2 target = other.transform.position;
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
			Vector2 target = targets.ElementAt(0);
			rope.target = target;
			grabs.Add(target, rope);
			ropes.Remove(rope);
			targets.Remove(target);
		}
		foreach (Rope rope in ropes) {
			rope.target = transform.position;
		}
	}
}
