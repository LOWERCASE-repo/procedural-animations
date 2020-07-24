#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class Core : MonoBehaviour {
	
	[SerializeField]
	Camera cam;
	
	[SerializeField]
	Rigidbody2D body;
	[SerializeField]
	CircleCollider2D circle;
	[SerializeField]
	float speed;
	HashSet<Probe> ropes = new HashSet<Probe>();
	internal void AddRope(Probe rope) { ropes.Add(rope); }
	HashSet<Rigidbody2D> targets = new HashSet<Rigidbody2D>();
	Dictionary<Rigidbody2D, Probe> grabs = new Dictionary<Rigidbody2D, Probe>();
	int connections;
	
	[SerializeField]
	Transform face;
	[SerializeField]
	SpriteRenderer leftEye;
	[SerializeField]
	SpriteRenderer rightEye;
	Vector2 facePos { set => face.transform.localPosition = value; }
	float faceAlpha {
		set {
			Color color = leftEye.color;
			color.a = value;
			leftEye.color = color;
			rightEye.color = color;
		}
	}
	
	void Start() {
		AssignGrabs();
	}
	
	void FixedUpdate() {
		float speed = this.speed;
		foreach (Probe rope in grabs.Values) {
			if (rope.body.bodyType == RigidbodyType2D.Static) {
				speed += this.speed;
			}
		}
		body.drag = speed / body.mass;
		Vector2 dir = Vector2.zero;
		if (Input.GetKey(KeyCode.Mouse0)) {
			dir = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - body.position;
		}
		dir = dir.normalized;
		Vector2 force = dir * speed * speed;
		body.AddForce(force);
		Vector2 intent = Vector2.ClampMagnitude(body.velocity / this.speed / this.speed, 1f);
		circle.offset = intent * circle.radius;
		facePos = new Vector2(intent.x * 0.7f, intent.y * 0.3f);
		faceAlpha = body.velocity.sqrMagnitude;
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
			Probe rope = grabs[target];
			rope.Release();
			ropes.Add(rope);
			grabs.Remove(target);
			AssignGrabs();
		}
	}
	
	void AssignGrabs() {
		while (ropes.Count > 0 && targets.Count > 0) {
			Probe rope = ropes.ElementAt(0);
			Rigidbody2D target = targets.ElementAt(0);
			rope.target = target;
			grabs.Add(target, rope);
			ropes.Remove(rope);
			targets.Remove(target);
		}
		foreach (Probe rope in ropes) {
			rope.target = body;
		}
	}
}
