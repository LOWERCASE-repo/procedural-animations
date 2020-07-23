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
	float speed, thrust;
	HashSet<Rope> ropes = new HashSet<Rope>();
	internal void AddRope(Rope rope) { ropes.Add(rope); }
	HashSet<Rigidbody2D> targets = new HashSet<Rigidbody2D>();
	Dictionary<Rigidbody2D, Rope> grabs = new Dictionary<Rigidbody2D, Rope>();
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
		body.drag = thrust;
		AssignGrabs();
	}
	
	void FixedUpdate() {
		Vector2 dir = Vector2.zero;
		if (Input.GetKey(KeyCode.Mouse0)) {
			dir = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - body.position;
		}
		float speed = 0f;
		foreach (Rope rope in grabs.Values) {
			if (rope.body.bodyType == RigidbodyType2D.Static) {
				speed += this.speed;
			}
		}
		Vector2 force = dir.normalized * thrust * speed;
		body.AddForce(force);
		Vector2 intent = Vector2.ClampMagnitude(body.velocity / this.speed * 0.3f, 1f);
		circle.offset = intent * circle.radius * 0.5f;
		facePos = new Vector2(intent.x * 0.8f, intent.y * 0.4f);
		faceAlpha = intent.magnitude * 2f;
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
