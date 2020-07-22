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
	Tendril[] tendrilRefs;
	HashSet<Tendril> tendrils;
	HashSet<Vector2> targets = new HashSet<Vector2>();
	Dictionary<Vector2, Tendril> grabs = new Dictionary<Vector2, Tendril>();
	int connections;
	
	void Start() {
		body.drag = thrust;
		tendrils = new HashSet<Tendril>(tendrilRefs);
		tendrilRefs = null;
	}
	
	void FixedUpdate() {
		Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		float bonusSpeed = 0f;
		foreach (Tendril tendril in tendrils) {
			if (tendril.attached) bonusSpeed += speedBonus;
		}
		Vector2 force = dir.normalized * thrust * (speed + speedBonus);
		body.AddForce(force);
		Debug.Log(targets.Count + " " + tendrils.Count + " " + grabs.Count);
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
			Tendril tendril = grabs[target];
			tendril.Release();
			tendrils.Add(tendril);
			grabs.Remove(target);
			AssignGrabs();
		}
	}
	
	void AssignGrabs() {
		while (tendrils.Count > 0 && targets.Count > 0) {
			Tendril tendril = tendrils.ElementAt(0);
			Vector2 target = targets.ElementAt(0);
			tendril.target = target;
			grabs.Add(target, tendril);
			tendrils.Remove(tendril);
			targets.Remove(target);
		}
		foreach (Tendril tendril in tendrils) {
			tendril.target = transform.position;
		}
	}
}
