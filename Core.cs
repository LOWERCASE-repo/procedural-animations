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
		float bonusSpeed = 0f;
		foreach (Rope rope in grabs.Values) {
			if (rope.body.bodyType == RigidbodyType2D.Static) {
				bonusSpeed += speedBonus;
			}
		}
		Debug.Log(bonusSpeed);
		Vector2 force = dir.normalized * thrust * (speed + bonusSpeed);
		body.AddForce(force);
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
